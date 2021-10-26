<template>
  <v-app id="inspire">
    <v-app-bar app color="gray" flat elevation="1">
      <v-container class="py-0 fill-height">
        <v-avatar class="mr-10" color="grey darken-1" size="32">
              <v-img :src="img" contain class="img-b" />
        </v-avatar>
        <span class="text">Time Clock</span>

        <v-spacer></v-spacer>

      </v-container>
    </v-app-bar>

    <v-main class="grey lighten-3">
      <Home />
    </v-main>
  </v-app>
</template>

<script>
import Home from "./components/Home";

export default {
  name: "App",

  components: {
    Home,
  },

  data: () => ({
    img: require("./assets/avatar.png")
  }),
};
</script>
