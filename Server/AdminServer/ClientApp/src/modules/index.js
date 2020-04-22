import { combineReducers } from 'redux';
import dashboard from './dashboard';
import auth from './auth';
import managers from './managers';
import users from './users';
import videos from './videos';

const rootReducer = combineReducers({
    dashboard,
    auth,
    managers,
    users,
    videos
});

export default rootReducer;