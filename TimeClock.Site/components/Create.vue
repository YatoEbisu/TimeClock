<template>
  <v-card>
    <v-card-title>
      <span class="text-h5">New hours</span>
    </v-card-title>

    <v-card-text>
      <v-container>
        <v-row>
          <v-col cols="12">
            <MenuDatePicker
              :hasValue="Clear"
              labelName="Day"
              @modelDPUpdate="UpdatePointDate"
            />
          </v-col>
          <v-col cols="12">
            <MenuTimePicker
              :hasValue="Clear"
              labelName="Entry Time"
              @modelTPUpdate="UpdateEntryTime"
            />
          </v-col>
          <v-col cols="12">
            <MenuTimePicker
              :hasValue="Clear"
              labelName="Exit Time"
              @modelTPUpdate="UpdateExitTime"
            />
          </v-col>
        </v-row>
      </v-container>
    </v-card-text>

    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn color="blue darken-1" text @click="SetdialogCreate(false)">
        Cancel
      </v-btn>
      <v-btn color="blue darken-1" text @click="Save"> Save </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
import { mapActions } from 'vuex'
export default {
  name: 'Create',
  data: () => ({
    Clear: false,
    defaultItem: {
      PointDate: null,
      EntryTime: null,
      ExitTime: null,
    },
  }),
  methods: {
    ...mapActions(['ShowSnackbar', 'SetdialogCreate', 'Get']),
    UpdateEntryTime(value) {
      this.defaultItem.EntryTime = value
    },
    UpdateExitTime(value) {
      this.defaultItem.ExitTime = value
    },
    UpdatePointDate(value) {
      this.defaultItem.PointDate = value
    },
    async Save() {
      if (
        this.defaultItem.PointDate != null &&
        this.defaultItem.EntryTime != null &&
        this.defaultItem.ExitTime != null
      )
        this.$axios
          .post('/TimeClock', {
            PointDate: this.defaultItem.PointDate,
            EntryTime: this.defaultItem.EntryTime,
            ExitTime: this.defaultItem.ExitTime,
          })
          .then((response) => {
            if (response.status == 201) {
              this.ClearFields()
              this.Get()
              this.SetdialogCreate(false)
              this.ShowSnackbar({
                icon: 'mdi-chevron-down-circle',
                color: 'success',
                msg: 'successfully created',
              })
            }
          })
          .catch((error) => {
            this.ShowSnackbar({
              icon: 'mdi-close-circle',
              color: 'red',
              msg: 'failed to create',
            })

            console.error(error)
          })
    },
    ClearFields() {
      this.defaultItem.PointDate = null
      this.defaultItem.EntryTime = null
      this.defaultItem.ExitTime = null
      this.Clear = true
    },
  },
}
</script>

<style>
</style>