<template>
  <v-snackbar
    :timeout="snackbar.timeout"
    :value="snackbar.show"
    :color="snackbar.color"
    style="z-index: 9 !important"
    absolute
    right
    top
  >
    <v-icon dark left>{{ snackbar.icon }}</v-icon>
    {{ snackbar.text }}
  </v-snackbar>
</template>

<script>
import { mapGetters } from 'vuex'
export default {
  name: 'Snackbar',
  computed: {
    ...mapGetters(['snackbar']),
  },
}
</script>