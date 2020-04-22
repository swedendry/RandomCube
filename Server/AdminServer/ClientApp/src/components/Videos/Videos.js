import React, { useState, useEffect } from 'react';
import BaseTable from '../Common/BaseTable';

import useUsers from '../../hooks/useVideos';

export default function Main() {
    const { view, onGet, onAdd, onUpdate, onDel, onClear } = useUsers();

    useEffect(() => {
        onGet();
    }, []);

    useEffect(() => {
        return () => {
            onClear();
        }
    }, []);

    const [columns, setColumns] = useState([
        {
            title: 'Id', field: 'id',
            editable: 'never'
        },
        {
            title: 'Title', field: 'title',
        },
        {
            title: 'Uri', field: 'uri',
        },
    ]);

    return (
        <BaseTable
            columns={columns}
            view={view.entries}
            editable={{
                onRowAdd: newData => onAdd(newData),
                onRowUpdate: (newData, oldData) => onUpdate(oldData.id, newData),
                onRowDelete: oldData => onDel(oldData.id),
            }}
        />
    );
}