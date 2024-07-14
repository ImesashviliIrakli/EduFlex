import "./enrollments.css";
import { useGet } from "../../hooks/useGet";
import { useNavigate } from "react-router-dom";
import { Table, message } from 'antd';
import { useRootContext } from "../../hooks/useRootContext";
import { useAlertBarContext } from "../../hooks/useAlertBarContext";
import Button from '@mui/material/Button';
import IconButton from '@mui/material/IconButton';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import AddIcon from '@mui/icons-material/Add';
import { fetchData } from "../../functions/fetchData";
import { useMutation, useQueryClient } from "@tanstack/react-query";

function Enrollments() {
    const { data: enrollments, isLoading } = useGet(["Enrollments"], "/api/Enrollment");
    const navigate = useNavigate();
    const queryClient = useQueryClient();
    const { baseUrl } = useRootContext();
    const {
        snackbarStatus,
        setSnackbarStat,
        setSnackbarMessage,
        handleSnackbarOpen,
    } = useAlertBarContext();

    const columns = [
        {
            title: "Index",
            dataIndex: "id",
            key: "id",
            sorter: {
                compare: (a, b) => a.id - b.id,
            },
        },
        {
            title: "StudentId",
            dataIndex: "studentId",
            key: "studentId",
            sorter: {
                compare: (a, b) => a.title.localeCompare(b.title),
            },
        },
        {
            title: "TeacherCourseMapId",
            dataIndex: "teacherCourseMapId",
            key: "teacherCourseMapId",
            sorter: {
                compare: (a, b) => a.title.localeCompare(b.title),
            },
        },
        {
            title: "Date",
            dataIndex: "date",
            key: "date",
            sorter: {
                compare: (a, b) => a.price - b.price,
            },
        },
        {
            title: "Status",
            dataIndex: "status",
            key: "status",
            sorter: {
                compare: (a, b) => a.title.localeCompare(b.title),
            },
        },
    ];

    const onChange = (pagination, filters, sorter, extra) => {
        console.log('params', pagination, filters, sorter, extra);
    };

    const processedenrollments = enrollments?.result?.map((course) => ({
        ...course,
        key: course.id,
    }));

    return (
        <div className="enrollments">
            <div className="enrollments-header d-flex align-items-center justify-content-between">
                <h1>Enrollments</h1>
            </div>

            <Table
                dataSource={processedenrollments}
                columns={columns}
                loading={isLoading}
                onChange={onChange}
                pagination={{
                    pageSize: 10,
                }}
                scroll={{
                    y: "100%",
                }}
            />
        </div>
    );
}

export default Enrollments;
