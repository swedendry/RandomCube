import axios from 'axios';

const ERROR = 'managers/ERROR';
const GET = 'managers/GET';
const ADD = 'managers/ADD';
const UPDATE = 'managers/UPDATE'
const DEL = 'managers/DEL';
const CLEAR = 'managers/CLEAR';

export const get = () => async (dispatch) => {
    try {
        const res = await axios.get('/api/managers');

        dispatch({ type: GET, payload: res.data.data })
    }
    catch (e) {
        dispatch({ type: ERROR })
    }
}

export const add = (entity) => async (dispatch) => {
    try {
        const res = await axios.post('/api/managers', entity);

        dispatch({ type: ADD, payload: res.data.data })
    }
    catch (e) {
        dispatch({ type: ERROR })
    }
}

export const update = (id, entity) => async (dispatch) => {
    try {
        const res = await axios.put(`/api/managers/${id}`, entity);

        dispatch({ type: UPDATE, payload: res.data.data })
    }
    catch (e) {
        dispatch({ type: ERROR })
    }
}

export const del = (id) => async (dispatch) => {
    try {
        const res = await axios.delete(`/api/managers/${id}`);

        dispatch({ type: DEL, payload: res.data.data })
    }
    catch (e) {
        dispatch({ type: ERROR })
    }
}

const initialState = {
    entries: null,
    loading: true,
};

export const clear = () => async (dispatch) => {
    try {
        dispatch({ type: CLEAR })
    }
    catch (e) {
        dispatch({ type: ERROR })
    }
}

const managers = (state = initialState, action) => {
    switch (action.type) {
        case GET:
            return {
                ...state,
                entries: action.payload,
                loading: false,
            };
        case ADD:
            return {
                ...state,
                entries: [action.payload, ...state.entries],
                loading: false
            };
        case UPDATE:
            return {
                ...state,
                entries: state.entries.map(entity => entity.id === action.payload.id ? action.payload : entity),
                loading: false
            };
        case DEL:
            return {
                ...state,
                entries: state.entries.filter(entity => entity.id !== action.payload)
            };
        case CLEAR:
            return {
                ...state,
                entries: null,
            };
        case ERROR:
            return {
                ...state,
            };
        default:
            return state;
    }
};

export default managers;