<template>
  <v-menu
    ref="menu"
    :close-on-content-click="false"
    :nudge-right="40"
    :return-value.sync="localModel"
    transition="scale-transition"
    offset-y
    max-width="290px"
    min-width="290px"
  >
    <template v-slot:activator="{ on, attrs }">
      <v-text-field
        v-model="localModel"
        :label="labelName"
        prepend-icon="mdi-clock-time-four-outline"
        readonly
        v-bind="attrs"
        v-on="on"
        required
        :rules="rules.time"
      ></v-text-field>
    </template>
    <v-time-picker v-model="localModel" full-width ampm-in-title>
      <v-spacer></v-spacer>
      <v-btn text color="primary" @click="$refs.menu.save(localModel)">
        OK
      </v-btn>
    </v-time-picker>
  </v-menu>
</template>

<script>
export default {
  name: 'MenuTimePicker',
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
      time: [(val) => (val || '').length > 0 || 'This field is required'],
    },
  }),
  watch: {
    localModel(value) {
      this.$emit('modelTPUpdate', value)
    },
    hasValue(value) {
      if (value == true) this.localModel = null
    },
  },
  methods: {},
}
</script>
