import React from 'react';
import ReactDOM from 'react-dom/client';
import 'bootstrap/dist/css/bootstrap.min.css';
import './css/index.css';
import reportWebVitals from './reportWebVitals';
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import Dasboard from './pages/Dashboard';
import Groups from './pages/Groups';
import Manage from './pages/Manage';
import Payment from './pages/Payment';
import Profile from './pages/Profile';
import Project from './pages/Project';
import Statistics from './pages/Statistics';
import Login from './pages/Login';
import Sidebar from './component/Sidebar';
import Employee from './pages/Manage/Employee';
import Department from './pages/Manage/Department';
import Jobs from './pages/Manage/Jobs';
import Timeline from './pages/Timeline';
import Report from './pages/Manage/Report';
import Allowance from './pages/Manage/Allowance';
import Payslip from './pages/Payment/Payslip';
import OverTime from './pages/Manage/OverTime';
import DailySalaryDetail from './pages/Payment/DailySalaryDetail';


const router = createBrowserRouter([
  {
    path: "/",
    element: <Sidebar/>,
    children: [ {
      path: "",
      element: <Dasboard/>,
    },
    {path: "profile",element: <Profile/>},
    {path: "project",element: <Project/>},
    {path: "manage",element: <Manage/>,children:
    [
        {path: "employee",element: <Employee/>},
        {path: "department",element: <Department/>},
        {path: "jobs", element: <Jobs/>},
        {path: "report", element: <Report/>},
        {path: "allowance", element: <Allowance/>},
        {path: "overtime", element: <OverTime/>},
                ]},
    {path: "timeline",element: <Timeline/>},
    {path: "groups",element: <Groups/>},
    {path: "payment",element: <Payment/>,children:
    [
      {path: "payslip",element: <Payslip/>},
      {path: "dailysalarydetail",element: <DailySalaryDetail/>},
              ]},
    ]
  },
  {
    path: "login",
    element: <Login/>,
  },
]);

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
