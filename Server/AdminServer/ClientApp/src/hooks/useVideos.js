import { useCallback } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { get, add, update, del, clear } from '../modules/videos';

export default function useVideos() {
    const view = useSelector(state => state.videos, []);
    const dispatch = useDispatch();

    const onGet = useCallback(() => dispatch(get()), [dispatch]);
    const onAdd = useCallback((entity) => dispatch(add(entity)), [dispatch]);
    const onUpdate = useCallback((id, entity) => dispatch(update(id, entity)), [dispatch]);
    const onDel = useCallback((id) => dispatch(del(id)), [dispatch]);
    const onClear = useCallback(() => dispatch(clear()), [dispatch]);

    return {
        view,
        onGet,
        onAdd,
        onUpdate,
        onDel,
        onClear,
    };
}