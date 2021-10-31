<template>
  <v-container>
    <v-row class="justify-center mt-5">
      <v-col cols="8">
        <v-row class="justify-center">
            <v-sheet
              v-if="!tabIndex"
              rounded="lg"
              elevation="0"
              color="transparent"
            >
              <List />
            </v-sheet>
            <v-sheet v-if="tabIndex" rounded="lg" max-width="500" min-height="350">
              <Export />
            </v-sheet>
        </v-row>
      </v-col>
    </v-row>
    <Snackbar />
  </v-container>
</template>
<script>
import { mapActions } from 'vuex'

export default {
  name: 'index',
  methods: {
    ...mapActions(['SettabIndex']),
  },
  computed: {
    tabIndex: {
      get() {
        return this.$store.state.tabIndex
      },
      set(value) {
        this.SettabIndex(value)
      },
    },
  },
}
</script>

