import "./courses.css";
import { useNavigate } from "react-router-dom";
import { useMutation } from "@tanstack/react-query";
import { useRootContext } from "../../hooks/useRootContext";
import { useAlertBarContext } from "../../hooks/useAlertBarContext";
import { useGet } from "../../hooks/useGet";
import { fetchData } from "../../functions/fetchData";
import { Table } from "antd";
import Button from "@mui/material/Button";
import IconButton from "@mui/material/IconButton";
import DeleteIcon from "@mui/icons-material/Delete";
import EditIcon from "@mui/icons-material/Edit";
import AddIcon from "@mui/icons-material/Add";

function Courses() {
  const { baseUrl } = useRootContext();
  const {
    snackbarStatus,
    setSnackbarStat,
    setSnackbarMessage,
    handleSnackbarOpen,
  } = useAlertBarContext();
  const navigate = useNavigate();

  const {
    data: courses,
    isLoading,
    refetch,
  } = useGet(["Courses"], "/api/course/get");

  const deleteMutation = useMutation({
    mutationKey: ["Delete Course"],
    mutationFn: (id) =>
      fetchData({
        url: baseUrl + `/api/course/${id}`,
        method: "delete",
      }),
    onSuccess: () => {
      handleSnackbarOpen();
      setSnackbarStat(snackbarStatus.success);
      setSnackbarMessage("Course Deleted Successfully!");
      refetch();
    },
    onError: () => {
      handleSnackbarOpen();
      setSnackbarStat(snackbarStatus.error);
      setSnackbarMessage("Course Delete Failed!");
    },
  });

  const handleDelete = (id) => {
    deleteMutation.mutate(id);
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
      render: (data) => data.faculty?.name,
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

  return (
    <div className="courses">
      <div className="courses-header d-flex align-items-center justify-content-between">
        <h1>Courses</h1>
        <Button
          variant="contained"
          startIcon={<AddIcon />}
          onClick={() => navigate("/create-course")}
          color="success"
        >
          Create Course
        </Button>
      </div>

      <Table
        rowKey="id"
        dataSource={courses?.result}
        columns={columns}
        loading={isLoading}
        pagination={{
          pageSize: 8,
        }}
      />
    </div>
  );
}

export default Courses;
