import "./faculties.css";
import { useNavigate } from "react-router-dom";
import { useGet } from "../../hooks/useGet";
import { Table } from 'antd';
import Button from '@mui/material/Button';
import IconButton from '@mui/material/IconButton';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import AddIcon from '@mui/icons-material/Add';

function Faculties() {
  const { data: faculties, isLoading } = useGet(["Faculties"], "/api/faculty/get");
  const navigate = useNavigate();

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
