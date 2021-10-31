<template>
  <v-app id="inspire">
    <v-app-bar
      app
      color="white"
      flat
    >
      <v-avatar
        :color="$vuetify.breakpoint.smAndDown ? 'grey darken-1' : 'transparent'"
        size="32"
      ></v-avatar>

      <v-tabs
        centered
        class="ml-n9"
        color="grey darken-1"
      >
        <v-tab @click="SettabIndex(false)"> Home </v-tab>
        <v-tab @click="SettabIndex(true)"> Export CSV </v-tab>
      </v-tabs>

      <v-avatar
        class="hidden-sm-and-down"
        color="grey darken-1 shrink"
        size="32"
      ></v-avatar>
    </v-app-bar>
    <v-main class="grey lighten-3">
      <Nuxt/>
    </v-main>
  </v-app>
</template>

<script>
import { mapActions } from 'vuex'

  export default {
    data: () => ({
    }),
    methods:{
    ...mapActions(['SettabIndex']),
    }
  }
</script>