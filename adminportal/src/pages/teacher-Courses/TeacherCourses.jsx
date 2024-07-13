import "./teacher-courses.css";
import { useNavigate } from "react-router-dom";
import { useState } from "react";
import { useGet } from "../../hooks/useGet";
import { Table } from 'antd';
import IconButton from '@mui/material/IconButton';
import InfoIcon from '@mui/icons-material/Info';

function TeacherCourses() {
    const navigate = useNavigate();
    const { data: teacherCourses, isLoading } = useGet(["TeacherCourses"], `/api/TeacherCourse`);

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
            title: "CourseId",
            dataIndex: "courseId",
            key: "courseId",
            sorter: {
                compare: (a, b) => a.courseId - b.courseId,
            },
        },
        {
            title: "Course Title",
            dataIndex: "courseTitle",
            key: "courseTitle",
            render: (courseTitle) => <span key={courseTitle}>{courseTitle}</span>,
            sorter: {
                compare: (a, b) => a.courseName.localeCompare(b.courseName),
            },
        },
        {
            title: "TeacherId",
            dataIndex: "teacherId",
            key: "teacherId",
            sorter: {
                compare: (a, b) => a.teacherId - b.teacherId,
            },
        },
        {
            title: "Teacher Email",
            dataIndex: "teacherEmail",
            key: "teacherEmail",
            render: (teacherEmail) => <span key={teacherEmail}>{teacherEmail}</span>,
            sorter: {
                compare: (a, b) => a.teacherEmail.localeCompare(b.teacherEmail),
            },
        },
        {
            title: "Action",
            key: "action",
            render: (data) => (
                <>
                    <IconButton
                        aria-label="details"
                        color="primary"
                        onClick={() => navigate(`/teachercourse-details/${data.id}`)}
                    >
                        <InfoIcon />
                    </IconButton>
                </>
            ),
        },
    ];

    const onChange = (pagination, filters, sorter, extra) => {
        console.log("params", pagination, filters, sorter, extra);
    };

    const processedTeacherCourses = teacherCourses?.result?.map((course) => ({
        ...course,
        key: course.id,
        courseTitle: course.course?.title, // Adjust according to your API response structure
        teacherEmail: course.teacher?.email, // Adjust according to your API response structure
    }));

    return (
        <div className="teacher-courses">
            <div className="teacher-courses-header d-flex align-items-center justify-content-between">
                <h1>Teacher Course Map</h1>
            </div>

            <Table
                dataSource={processedTeacherCourses}
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

export default TeacherCourses;
