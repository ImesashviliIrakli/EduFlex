import "./courses.css"
import { useGet } from "../../hooks/useGet";
import { Table } from 'antd';
import Button from '@mui/material/Button';
import IconButton from '@mui/material/IconButton';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import AddIcon from '@mui/icons-material/Add';

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
      compare: (a, b) => a.title - b.title,
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
    title: "Image",
    dataIndex: "image",
    key: "image",
  },
  {
    title: "Faculty",
    dataIndex: "faculty",
    key: "faculty",
    sorter: {
      compare: (a, b) => a.faculty - b.faculty,
    },
  },
  {
    title: "Action",
    key: "action",
    render: () => (
      <>
        <IconButton aria-label="edit" color="success">
          <EditIcon />
        </IconButton>
        <IconButton aria-label="delete" color="error">
          <DeleteIcon />
        </IconButton>
      </>
    ),
  },
];

const onChange = (pagination, filters, sorter, extra) => {
  console.log('params', pagination, filters, sorter, extra);
};

function Courses() {
  const { data: courses, isLoading } = useGet(["Courses"], "/api/course/get");

  console.log(courses);
  return (
    <div className="courses">
      <div className="courses-header d-flex align-items-center justify-content-between">
        <h1>Courses</h1>
        <Button variant="contained" startIcon={<AddIcon />}>
          Create Course
        </Button>
      </div>

      <Table
        dataSource={courses?.result}
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

export default Courses
