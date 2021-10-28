<template>
  <v-container>
    <v-row class="justify-center">
      <v-col cols="10" class="">
        <v-sheet
          min-height="50vh"
          rounded="lg"
          style="padding: 15px"
          v-if="!seen"
        >
          <div class="c-header">
            <h1>Punch a clock</h1>
          </div>
          <v-spacer></v-spacer>
          <v-row>
            <v-col cols="6">
              <v-row style="margin-top: 50px">
                <v-col cols="12" class="d-flex">
                  <v-text-field
                    required
                    solo
                    label="Date"
                    disabled
                    shaped
                    class="zindex-1"
                    v-model="date"
                  ></v-text-field>
                  <v-fab-transition>
                    <v-btn @click.stop="dialogDate = true" class="b-icon" icon>
                      <v-icon color="red">mdi-calendar</v-icon>
                    </v-btn>
                  </v-fab-transition>
                </v-col>
                <v-col cols="12" class="d-flex">
                  <v-text-field
                    required
                    solo
                    label="Entry Time"
                    disabled
                    shaped
                    class="zindex-1"
                    v-model="entryTime"
                  ></v-text-field>
                  <v-fab-transition>
                    <v-btn
                      @click.stop="dialogEntryTime = true"
                      class="b-icon"
                      icon
                    >
                      <v-icon color="red">mdi-alarm</v-icon>
                    </v-btn>
                  </v-fab-transition>
                </v-col>
                <v-col cols="12" class="d-flex">
                  <v-text-field
                    required
                    solo
                    label="Exit Time"
                    elevation="2"
                    disabled
                    shaped
                    class="zindex-1"
                    v-model="exitTime"
                  ></v-text-field>
                  <v-fab-transition>
                    <v-btn
                      @click.stop="dialogExitTime = true"
                      class="b-icon"
                      icon
                    >
                      <v-icon color="red">mdi-alarm</v-icon>
                    </v-btn>
                  </v-fab-transition>
                </v-col>
                <v-col cols="12">
                  <v-btn
                    @click="Save"
                    class="ma-2 text-white"
                    color="primary"
                    light
                    x-large
                    width="200"
                    elevation="2"
                  >
                    Save
                  </v-btn>
                </v-col>
              </v-row>
            </v-col>
            <v-col cols="6">
              <div class="div-img">
                <v-img :src="img" contain class="img-b" />
              </div>
            </v-col>
          </v-row>
        </v-sheet>

        <v-sheet
          min-height="50vh"
          rounded="lg"
          style="padding: 15px"
          v-if="seen"
        >
          <div class="c-header">
            <h1>Appointed hours</h1>
          </div>
          <v-data-table
            :headers="headers"
            :items="data"
            :items-per-page="10"
            class="mt-7"
          >
            <template v-slot:item.actions="{ item }">
              <v-icon small class="mr-2" @click="editItem(item)">
                mdi-pencil
              </v-icon>
              <v-icon small @click="DeleteConfirm(item.id)">
                mdi-delete
              </v-icon>
            </template>
            <!-- <template v-slot:no-data>
              <v-btn color="primary" @click="initialize"> Reset </v-btn>
            </template> -->
          </v-data-table>
        </v-sheet>
      </v-col>
    </v-row>
    <v-dialog v-model="dialogDate" max-width="290">
      <v-card>
        <v-date-picker v-model="date"></v-date-picker>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="red darken-1" text @click="dialogDate = false">
            Close
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-dialog v-model="dialogEntryTime" max-width="290">
      <v-card>
        <v-time-picker
          v-model="entryTime"
          full-width
          format="ampm"
        ></v-time-picker>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            color="red darken-1"
            text
            class="mt-5"
            @click="dialogEntryTime = false"
          >
            Close
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-dialog v-model="dialogExitTime" max-width="290">
      <v-card>
        <v-time-picker
          v-model="exitTime"
          full-width
          format="ampm"
        ></v-time-picker>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            color="red darken-1"
            text
            class="mt-5"
            @click="dialogExitTime = false"
          >
            Close
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-dialog v-model="dialogDelete" max-width="500">
      <v-card>
        <v-card-title class="text-h5">
          Are you sure you want to delete this item?
        </v-card-title>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" text @click="dialogDelete = false"
            >Cancel</v-btn
          >
          <v-btn color="red darken-1" text @click="Delete(itemDeleteId)"
            >Delete</v-btn
          >
          <v-spacer></v-spacer>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-snackbar
      :timeout="snackbar.timeout"
      :value="snackbar.show"
      :color="snackbar.color"
      absolute
      right
      top
    >
      <v-icon dark left>{{ snackbar.icon }}</v-icon>
      {{ snackbar.text }}
    </v-snackbar>
  </v-container>
  <!-- <v-container>
    <v-card>
       
    </v-card>
   
   
  </v-container> -->
</template>


<script>
import axios from "axios";

var ENDPOINT_API = "http://localhost:5000/api/TimeClock";

export default {
  name: "Home",

  data: () => ({
    snackbar: {
      show: false,
      text: "",
      timeout: 2000,
      color: "",
      icon: ""
    },
    itemDeleteId: null,
    img: require("../assets/img-dev.png"),
    seen: false,
    date: null,
    data: null,
    entryTime: null,
    exitTime: null,
    dialogDelete: false,
    dialogDate: false,
    dialogEntryTime: false,
    dialogExitTime: false,
    headers: [
      { text: "Day", align: "start", value: "PointDate" },
      { text: "Entry Time", value: "EntryTime" },
      { text: "Exit Time", value: "ExitTime" },
      { text: "Actions", value: "actions", sortable: false },
    ],
  }),
  methods: {
    Get() {
      axios
        .get(ENDPOINT_API, {
          headers: {
            "Access-Control-Allow-Origin": "*",
            "Content-Type": "application/json",
          },
        })
        .then((response) => {
          this.data = response.data.map((x) => {
            x.EntryTime = x.EntryTime.substring(3);
            x.ExitTime = x.ExitTime.substring(3);
            return x;
          });
        })
        .catch((error) => {
          console.error(error);
        });
      //.finally(() => this.loading = false)
    },
    Save() {
      console.log("entrou");
      if (this.date != null && this.entryTime != null && this.exitTime != null)
        axios
          .post(ENDPOINT_API, {
            PointDate: this.date,
            EntryTime: this.entryTime,
            ExitTime: this.exitTime,
          })
          .then((response) => {
            console.log(response);
            if (response.status == 201) {
              this.ShowSnackbar("successfully created", "success", "mdi-chevron-down-circle");
              this.ClearFields();
              this.Get();
            }
          })
          .catch((error) => {
            this.ShowSnackbar("failed to create", "red", "mdi-close-circle");
            console.error(error);
          });
      else {
        this.ShowSnackbar("All fields are required", "warning", "mdi-alert-circle");
      }
    },
    DeleteConfirm(id) {
      this.itemDeleteId = id;
      this.dialogDelete = true;
    },
    Delete(id) {
      console.log("entrou delete");
      this.itemDeleteId = null;
      this.dialogDelete = false;
      axios
        .delete(ENDPOINT_API + `/id:guid?id=${id}`)
        .then((response) => {
          console.log(response.status);
          if (response.status == 200) {
            console.log("deletou");
            this.ShowSnackbar("Successfully deleted item", "success", "mdi-alert-circle");
            this.Get();
          }
        })
        .catch((error) => {
          this.ShowSnackbar("failed to deleted item", "red", "mdi-close-circle");
          console.error(error);
        });
    },
    ShowSnackbar(msg, color, icon) {
      this.snackbar.icon = icon;
      this.snackbar.text = msg;
      this.snackbar.color = color;
      this.snackbar.show = true;
      setTimeout(() => {
        this.snackbar.show = false;
      }, this.snackbar.timeout);
    },
    ClearFields() {
      this.date = "";
      this.entryTime = "";
      this.exitTime = "";
    },
  },
  mounted() {
    this.Get();
  },
};
</script>
<style scoped>
.b-icon {
  margin-left: -40px;
  margin-top: 7px;
  z-index: 2;
}
.zindex-1 {
  z-index: 1;
}
.m-5 {
  margin: 12px;
}
.c-header {
  margin: -15px;
  padding: 20px;
  background-color: #b218b7a6;
  border-top-left-radius: 5px;
  border-top-right-radius: 5px;
}
.c-header h1 {
  color: white;
}
.line {
  border-bottom: #c4c4c4 solid 2px;
  margin: 0 15px;
}
.img-b {
  max-width: 550px;
  margin-left: 80px;
  margin-top: -50px;
}
.text-white {
  color: white;
}
.div-img {
  display: grid;
  place-items: center;
}
</style>
