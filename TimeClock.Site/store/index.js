const timeout = 2000

export const state = () => ({
    snackbar: {
        show: false,
        text: '',
        timeout: timeout,
        color: '',
        icon: '',
    },
    data: [],
    dialogCreate: false,
    tabIndex: false,
})

export const getters = {
    snackbar(state) {
        return state.snackbar
    },
    data(state) {
        return state.data
    },
    tabIndex(state) {
        return state.tabIndex
    },
    dialogCreate(state) {
        return state.dialogCreate
    }
}

export const mutations = {
    Setsnackbar(state, payload) {
        state.snackbar = payload
    },
    SetdialogCreate(state, payload) {
        state.dialogCreate = payload
    },
    SettabIndex(state, payload) {
        state.tabIndex = payload
    },
    ShowSnackbar(state, payload) {
        state.snackbar.icon = payload.icon
        state.snackbar.text = payload.msg
        state.snackbar.color = payload.color
        state.snackbar.show = true
    },
    HideSnackbar(state) {
        state.snackbar.show = false
    },
    Get(state, payload) {
        state.data = payload
    },
}

export const actions = {
    Setsnackbar({ commit }, payload) { commit('Setsnackbar', payload) },
    SettabIndex({ commit }, payload) { commit('SettabIndex', payload) },
    SetdialogCreate({ commit }, payload) { commit('SetdialogCreate', payload) },
    ShowSnackbar({ commit }, payload) {
        commit('ShowSnackbar', payload)
        setTimeout(() => {
            commit('HideSnackbar')
        }, timeout)

    },
    async Get({ commit }) {
        var response = await this.$axios.get('/TimeClock')

        commit('Get', response.data.map((x) => {
            x.EntryTime = x.EntryTime.substring(3);
            x.ExitTime = x.ExitTime.substring(3);

            var entryTime = new Date(new Date().toDateString() + ' ' + x.EntryTime)
            var exitTime = new Date(new Date().toDateString() + ' ' + x.ExitTime)
            var diff = (entryTime - exitTime) / 1000;
            diff = Math.abs(Math.floor(diff));

            var days = Math.floor(diff / (24 * 60 * 60));
            var leftSec = diff - days * 24 * 60 * 60;

            var hrs = Math.floor(leftSec / (60 * 60));
            var leftSec = leftSec - hrs * 60 * 60;

            var min = Math.floor(leftSec / (60));
            var leftSec = leftSec - min * 60;

            x.Hours = `${(hrs-1).toString().padStart(2, '0')}:${min.toString().padStart(2, '0')}`
            return x;
        }))
    }
}