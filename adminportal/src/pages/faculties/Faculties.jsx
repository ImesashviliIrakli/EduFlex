import "./faculties.css";
import { useNavigate } from "react-router-dom";
import { useGet } from "../../hooks/useGet";
import { Table } from 'antd';
import { useRootContext } from "../../hooks/useRootContext";
import { useAlertBarContext } from "../../hooks/useAlertBarContext";
import Button from '@mui/material/Button';
import IconButton from '@mui/material/IconButton';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import AddIcon from '@mui/icons-material/Add';
import { fetchData } from "../../functions/fetchData";
import { useMutation, useQueryClient } from "@tanstack/react-query";

function Faculties() {
    const { data: faculties, isLoading } = useGet(["Faculties"], "/api/faculty/get");

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
                url: baseUrl + `/api/faculty/${id}`,
                method: "delete"
            }),
        onSuccess: () => {
            handleSnackbarOpen();
            setSnackbarStat(snackbarStatus.success);
            setSnackbarMessage("Faculty Deleted Successfully!");
            queryClient.invalidateQueries(["Faculty"]);
        },
        onError: () => {
            handleSnackbarOpen();
            setSnackbarStat(snackbarStatus.error);
            setSnackbarMessage("Faculty Delete Failed!");
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
            title: "Name",
            dataIndex: "name",
            key: "name",
            sorter: {
                compare: (a, b) => a.name.localeCompare(b.name),
            },
        },
        {
            title: "Action",
            key: "action",
            render: (data) => (
                <>
                    <IconButton
                        aria-label="edit"
                        color="success"
                        onClick={() => navigate(`/edit-faculty/${data.id}`)}
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
        console.log("params", pagination, filters, sorter, extra);
    };

    return (
        <div className="faculties">
            <div className="faculties-header d-flex align-items-center justify-content-between">
                <h1>Faculties</h1>
                <Button variant="contained" startIcon={<AddIcon />} onClick={() => navigate("/create-faculty")}>
                    Create Faculty
                </Button>
            </div>

            <Table
                dataSource={faculties?.result}
                columns={columns}
                loading={isLoading}
                onChange={onChange}
                rowKey="id"
                pagination={{
                    pageSize: 8,
                }}
            />
        </div>
    );
}

export default Faculties
