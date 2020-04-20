import axios from 'axios';

const ERROR = 'users/ERROR';
const GET = 'users/GET';
const ADD = 'users/ADD';
const UPDATE = 'users/UPDATE'
const DEL = 'users/DEL';
const CLEAR = 'users/CLEAR';

export const get = () => async (dispatch) => {
    try {
        const res = await axios.get('/api/users');

        dispatch({ type: GET, payload: res.data.data })
    }
    catch (e) {
        dispatch({ type: ERROR })
    }
}

export const add = (entity) => async (dispatch) => {
    try {
        const res = await axios.post('/api/users', entity);

        dispatch({ type: ADD, payload: res.data.data })
    }
    catch (e) {
        dispatch({ type: ERROR })
    }
}

export const update = (id, entity) => async (dispatch) => {
    try {
        const res = await axios.put(`/api/users/${id}`, entity);

        dispatch({ type: UPDATE, payload: res.data.data })
    }
    catch (e) {
        dispatch({ type: ERROR })
    }
}

export const del = (id) => async (dispatch) => {
    try {
        const res = await axios.delete(`/api/users/${id}`);

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

const users = (state = initialState, action) => {
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

export default users;