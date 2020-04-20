import React from 'react';
import styled from 'styled-components';
import Videos from '../../components/Videos/Videos'

// Styles
const Title = styled.h1`
  color: #656565;
  font-size: 1.2rem;
`;

export default function Main() {
    return (
        <>
            <Title>Videos</Title>
            <Videos />
        </>
    );
}