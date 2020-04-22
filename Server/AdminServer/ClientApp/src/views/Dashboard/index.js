import React, { useEffect } from 'react';
import { IoLogoGithub, IoIosRocket } from 'react-icons/io';
import DashboardCard from '../../components/Common/DashboardCard';

export default function Main() {
    const Cards = [
        {
            backgroud: 'linear-gradient(to right, #6190E8 100%, #A7BFE8 80%, #6190E8 100%)',
            icon: <IoLogoGithub size={60} color="#fff" />,
            description: 'ClientSource(Github)',
            url: 'https://github.com/swedendry/thegido-client',
        },
        {
            backgroud: 'linear-gradient(to right, #6190E8 100%, #A7BFE8 80%, #6190E8 100%)',
            icon: <IoLogoGithub size={60} color="#fff" />,
            description: 'ServerSource(Github)',
            url: 'https://github.com/swedendry/thegido-server',
        },
        {
            backgroud: 'linear-gradient(to right, #ff7e5f 0%, #feb47b 100%, #ff7e5f 100%)',
            icon: <IoIosRocket size={60} color="#fff" />,
            description: 'Api(Swagger)',
            url: 'https://thegido-admin.azurewebsites.net/swagger/index.html',
        },
    ];

    /**
     * Main
     */

    // Mount
    useEffect(() => {
        document.title = 'thegido - Home';
    }, []);

    // Unmount
    useEffect(() => {
        return () => {
            document.title = '';
        };
    }, []);

    return <DashboardCard cards={Cards} />
}
