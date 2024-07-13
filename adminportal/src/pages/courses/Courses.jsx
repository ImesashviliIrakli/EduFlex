import "./courses.css";
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

function Courses() {
    const { data: courses, isLoading } = useGet(["Courses"], "/api/course/get");
    const navigate = useNavigate();
    const queryClient = useQueryClient();
    const { baseUrl } = useRootContext();
    const {
        snackbarStatus,
        setSnackbarStat,
        setSnackbarMessage,
        handleSnackbarOpen,
    } = useAlertBarContext();

    const mutation = useMutation({
        mutationFn: (id) =>
            fetchData({
                url: baseUrl + `/api/course/${id}`,
                method: "delete"
            }),
        onSuccess: () => {
            handleSnackbarOpen();
            setSnackbarStat(snackbarStatus.success);
            setSnackbarMessage("Course Deleted Successfully!");
            queryClient.invalidateQueries(["Courses"]);
        },
        onError: () => {
            handleSnackbarOpen();
            setSnackbarStat(snackbarStatus.error);
            setSnackbarMessage("Course Delete Failed!");
        },
    });

    const handleDelete = (id) => {
        mutation.mutate(id);
    };

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
            title: "Title",
            dataIndex: "title",
            key: "title",
            sorter: {
                compare: (a, b) => a.title.localeCompare(b.title),
            },
        },
        {
            title: "Description",
            dataIndex: "description",
            key: "description",
        },
        {
            title: "Price",
            dataIndex: "price",
            key: "price",
            sorter: {
                compare: (a, b) => a.price - b.price,
            },
        },
        {
            title: "ImageUrl",
            dataIndex: "imageUrl",
            key: "imageUrl",
        },
        {
            title: "Faculty",
            key: "faculty",
            render: (data) => <span key={data.faculty?.id}>{data.faculty?.name}</span>,
            sorter: {
                compare: (a, b) => a.faculty.name.localeCompare(b.faculty.name),
            },
        },
        {
            title: "Action",
            key: "action",
            render: (data) => (
                <>
                    <IconButton
                        aria-label="edit"
                        color="primary"
                        onClick={() => navigate(`/edit-course/${data.id}`)}
                    >
                        <EditIcon />
                    </IconButton>
                    <IconButton
                        aria-label="delete"
                        color="error"
                        onClick={() => handleDelete(data.id)}
                    >
                        <DeleteIcon />
                    </IconButton>
                </>
            ),
        },
    ];

    const onChange = (pagination, filters, sorter, extra) => {
        console.log('params', pagination, filters, sorter, extra);
    };

    const processedCourses = courses?.result?.map((course) => ({
        ...course,
        key: course.id,
    }));

    return (
        <div className="courses">
            <div className="courses-header d-flex align-items-center justify-content-between">
                <h1>Courses</h1>
                <Button variant="contained" startIcon={<AddIcon />} onClick={() => navigate("/create-course")} color="success">
                    Create Course
                </Button>
            </div>

            <Table
                dataSource={processedCourses}
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

export default Courses;
