import "./users.css";
import { useNavigate } from "react-router-dom";
import { useState } from "react";
import { useGet } from "../../hooks/useGet";
import { Table } from 'antd';
import Button from '@mui/material/Button';
import IconButton from '@mui/material/IconButton';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import AddIcon from '@mui/icons-material/Add';
import ToggleButton from '@mui/material/ToggleButton';
import ToggleButtonGroup from '@mui/material/ToggleButtonGroup';

function Users() {
    const navigate = useNavigate();
    const [role, setRole] = useState('Teacher');
    const { data: users, isLoading } = useGet(["Users", role], `/api/auth/getusers/${role}`);

    const handleRoleChange = (event, newRole) => {
        if (newRole !== null) {
            setRole(newRole);
        }
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
            title: "Email",
            dataIndex: "email",
            key: "email",
            sorter: {
                compare: (a, b) => a.email.localeCompare(b.email),
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
                    // onClick={() => navigate(`/edit-faculty/${data.id}`)}
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
        <div className="users">
            <div className="users-header d-flex align-items-center justify-content-between">
                <h1>Users</h1>
                <Button variant="contained" startIcon={<AddIcon />} onClick={() => navigate("/create-user")}>
                    Create User
                </Button>
            </div>

            <div className="usertable-filter d-flex align-items-center justify-content-between">
                <ToggleButtonGroup
                    value={role}
                    exclusive
                    onChange={handleRoleChange}
                    aria-label="role selection"
                >
                    <ToggleButton value="Teacher" aria-label="teacher">
                        Teacher
                    </ToggleButton>
                    <ToggleButton value="Student" aria-label="student">
                        Student
                    </ToggleButton>
                </ToggleButtonGroup>
            </div>

            <Table
                dataSource={users?.result}
                columns={columns}
                loading={isLoading}
                onChange={onChange}
                rowKey="id"
                pagination={{
                    pageSize: 8,
                }}
            />
        </div>
    )
}

export default Users;
