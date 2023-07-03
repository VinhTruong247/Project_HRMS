import React from "react";
import { DataContext } from '../contexts/DataContext'

function useData() {
    return React.useContext(DataContext);
};

export default useData;