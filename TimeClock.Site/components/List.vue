<template>
  <div>
    <v-data-table
      :headers="headers"
      :items="data"
      :loading="loadingTable"
      sort-by="calories"
      class="elevation-1"
    >
      <template v-slot:top>
        <v-toolbar flat>
          <v-toolbar-title>Time Clock</v-toolbar-title>
          <v-divider class="mx-4" inset vertical></v-divider>
          <v-spacer></v-spacer>
          <!-- new item -->
          <v-dialog
            v-model="dialogCreate"
            max-width="500px"
            style="z-index: 8 !important"
          >
            <template v-slot:activator="{ on, attrs }">
              <v-btn color="primary" dark class="mb-2" v-bind="attrs" v-on="on">
                New hours
              </v-btn>
            </template>
            <Create />
          </v-dialog>
          <!-- delete -->
          <v-dialog v-model="dialogDelete" max-width="500px">
            <v-card>
              <v-card-title class="text-h5"
                >Are you sure you want to delete this item?</v-card-title
              >
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="blue darken-1" text @click="closeDelete"
                  >Cancel</v-btn
                >
                <v-btn color="blue darken-1" text @click="Delete(itemDeleteId)"
                  >OK</v-btn
                >
                <v-spacer></v-spacer>
              </v-card-actions>
            </v-card>
          </v-dialog>
        </v-toolbar>
      </template>

      <template v-slot:item.actions="{ item }">
        <v-icon small class="mr-2"> mdi-pencil </v-icon>
        <v-icon small @click="deleteConfirm(item.id)"> mdi-delete </v-icon>
      </template>
      <template v-slot:no-data>
        <v-btn color="primary" @click="Get"> Reload </v-btn>
      </template>
    </v-data-table>
  </div>
</template>

<script>
import { mapActions, mapGetters } from 'vuex'

export default {
  name: 'List',
  data: () => ({
    loadingTable: true,
    defaultItem: {
      PointDate: null,
      EntryTime: null,
      ExitTime: null,
    },
    dialogDelete: false,
    headers: [
      {
        text: 'Day',
        align: 'start',
        sortable: true,
        value: 'PointDate',
      },
      { text: 'Entry Time', value: 'EntryTime', sortable: false },
      { text: 'Exit Time', value: 'ExitTime', sortable: false },
      { text: 'Worked Time', value: 'Hours', sortable: false },
      { text: 'Actions', value: 'actions', sortable: false },
    ],
    itemDeleteId: null,
  }),
  computed: {
    ...mapGetters(['data']),
    dialogCreate: {
      get() {
        return this.$store.state.dialogCreate
      },
      set(value) {
        this.SetdialogCreate(value)
      },
    },
  },

  watch: {
    data(val){
      if(val.length > 0)
        this.loadingTable = false
    }
  },

  methods: {
    ...mapActions(['Get', 'SetdialogCreate', 'ShowSnackbar']),
    UpdateEntryTime(value) {
      this.defaultItem.EntryTime = value
    },
    UpdateExitTime(value) {
      this.defaultItem.ExitTime = value
    },
    UpdatePointDate(value) {
      this.defaultItem.PointDate = value
    },
    deleteConfirm(id) {
      this.itemDeleteId = id
      this.dialogDelete = true
    },
    async Delete(id) {
      this.itemDeleteId = null
      this.dialogDelete = false
      this.$axios
        .delete(`/TimeClock/id:guid?id=${id}`)
        .then((response) => {
          if (response.status == 200) {
            this.ShowSnackbar({
              msg: 'Successfully deleted item',
              color: 'success',
              icon: 'mdi-chevron-down-circle',
            })
            this.Get()
          }
        })
        .catch((error) => {
          this.ShowSnackbar({
            msg: 'failed to deleted item',
            color: 'red',
            icon: 'mdi-close-circle',
          })
          console.error(error)
        })
    },
    closeDelete() {
      this.dialogDelete = false
      //
    },
  },
  mounted() {},
  async created() {
    await this.Get()
  },
}
</script>

<style>
</style>