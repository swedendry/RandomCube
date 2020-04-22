import React from 'react';

import { delToken } from '../services/auth';

import Dashboard from '../views/Dashboard';
import Managers from '../views/Managers';
import Users from '../views/Users';
import Videos from '../views/Videos';

// Icons
import {
    IoMdOptions,
    IoIosKey,
    IoMdPeople,
    IoLogoYoutube
} from 'react-icons/io'

// Routes
const Routes = {
    dashboard: '/',
    managers: '/managers',
    users: '/users',
    videos: '/videos',
};

const routes = {
    navbar: {
        dropdown: {
            user: {
                avatar: 'https://i.imgur.com/NpICPSl.jpg',
                name: 'thegido',
                jobRole: 'Administrator',
            },

            buttons: {
                settings: {
                    name: 'Settings',
                    event: () => { }
                },
                profile: {
                    name: 'Profile',
                    event: () => { }
                },
                logout: {
                    name: 'Logout',
                    event: () => {
                        delToken();
                        document.location.reload();
                    }
                }
            }
        }
    },

    sidebar: {
        brand: {
            max: 'thegido',
            min: 'gd'
        },

        buttons: [
            {
                name: 'Dashboard',
                icon: <IoMdOptions />,
                route: Routes.dashboard,
            },
            {
                name: 'Managers',
                icon: <IoIosKey />,
                route: Routes.managers,
            },
            {
                name: 'Users',
                icon: <IoMdPeople />,
                route: Routes.users,
            },
            {
                name: 'Videos',
                icon: <IoLogoYoutube />,
                route: Routes.videos,
            },
        ]
    },

    content: [
        {
            route: Routes.dashboard,
            page: Dashboard
        },
        {
            route: Routes.managers,
            page: Managers
        },
        {
            route: Routes.users,
            page: Users
        },
        {
            route: Routes.videos,
            page: Videos
        }
    ]
};

export default routes;
