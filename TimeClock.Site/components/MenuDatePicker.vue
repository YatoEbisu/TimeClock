<template>
  <v-menu
    v-model="menu"
    :close-on-content-click="false"
    transition="scale-transition"
    offset-y
    max-width="290px"
    min-width="auto"
  >
    <template v-slot:activator="{ on, attrs }">
      <v-text-field
        v-model="localModel"
        :label="labelName"
        persistent-hint
        prepend-icon="mdi-calendar"
        v-bind="attrs"
        v-on="on"
        required
        :rules="rules.date"
      ></v-text-field>
    </template>
    <v-date-picker
      v-model="localModel"
      no-title
      @input="menu = false"
    ></v-date-picker>
  </v-menu>
</template>

<script>
export default {
  name: 'MenuDatePicker',
  props: {
    hasValue: {
      type: Boolean,
      required: true,
    },
    labelName: {
      type: String,
      required: true,
    },
  },
  data: () => ({
    menu: false,
    localModel: null,
    rules: {
      date: [(val) => (val || '').length > 0 || 'This field is required'],
    },
  }),
  watch: {
    localModel(value) {
      this.$emit('modelDPUpdate', value)
    },
    hasValue(value) {
      if (value == true) this.localModel = null
    },
  },
}
</script>

<style>
</style>