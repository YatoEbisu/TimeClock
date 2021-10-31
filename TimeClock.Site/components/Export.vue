<template>
  <v-container>
    <v-row class="justify-center mt-5">
      <v-col cols="8">
        <v-row>
          <v-col cols="12">
            <MenuDatePicker
              :hasValue="Clear"
              labelName="Start Date"
              @modelDPUpdate="UpdateStartDate"
            />
          </v-col>
          <v-col cols="12">
            <MenuDatePicker
              :hasValue="Clear"
              labelName="End Date"
              @modelDPUpdate="UpdateEndDate"
            />
          </v-col>
          <v-col cols="12">
            <v-btn @click="Download()" tile color="success" block>
              <v-icon left> mdi-tray-arrow-down </v-icon>
              Download
            </v-btn>
          </v-col>
        </v-row>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { mapActions } from 'vuex'

export default {
  data: () => ({
    Clear: false,
    export: {
      startDate: null,
      endDate: null,
    },
  }),
  methods: {
    ...mapActions(['ShowSnackbar']),
    UpdateStartDate(value) {
      this.export.startDate = value
    },
    UpdateEndDate(value) {
      this.export.endDate = value
    },
    async Download() {
      if (this.export.startDate != null && this.export.endDate != null)
        this.$axios
          .post('/TimeClock/Export', {
            StartDate: this.export.startDate,
            EndDate: this.export.endDate,
          })
          .then((response) => {
            if (response.status == 200) {
              const url = window.URL.createObjectURL(new Blob([response.data]))
              const link = document.createElement('a')
              link.href = url
              link.setAttribute('download', 'export.csv') //or any other extension
              document.body.appendChild(link)
              link.click()
            }

            if(response.status == 204)
                this.ShowSnackbar({
                icon: 'mdi-alert-circle',
                color: 'warning',
                msg: 'No data found',
              })
          })
          .catch((error) => {
              this.ShowSnackbar({
                icon: 'mdi-close-circle',
                color: 'red',
                msg: 'failed to export',
              })
            console.error(error)
          })
    },
  },
}
</script>

<style>
</style>