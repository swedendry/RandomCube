import React from 'react';
import styled from 'styled-components';
import Managers from '../../components/Managers/Managers'

// Styles
const Title = styled.h1`
  color: #656565;
  font-size: 1.2rem;
`;

export default function Main() {
    return (
        <>
            <Title>Managers</Title>
            <Managers />
        </>
    );
}