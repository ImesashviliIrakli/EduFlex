import "./courses.css";
import { useNavigate } from "react-router-dom";
import { useGet } from "../../hooks/useGet";
import { CardActionArea } from "@mui/material";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import CardMedia from "@mui/material/CardMedia";
import Typography from "@mui/material/Typography";
import Loader from "../../components/loader/Loader";

function Courses() {
  const navigate = useNavigate();

  const { data: courses, isLoading } = useGet(["Courses"], "/api/course/get");

  if (isLoading) return <Loader />;
  return (
    <div className="courses">
      <div className="courses-container d-flex justify-content-center align-items-center flex-wrap">
        {courses?.result.map((course) => (
          <Card
            key={course.id}
            sx={{ maxWidth: 345 }}
            className="course-card"
            onClick={() => navigate(`/course/${course.id}`)}
          >
            <CardActionArea>
              <CardMedia
                component="img"
                height="140"
                image={
                  course.imageUrl !== ""
                    ? course.imageUrl
                    : "https://salonlfc.com/wp-content/uploads/2018/01/image-not-found-1-scaled-1150x647.png"
                }
                alt={course.title + " course image"}
              />
              <CardContent>
                <Typography gutterBottom variant="h5" component="div">
                  {course.title}
                </Typography>
                <Typography variant="body2" color="text.secondary">
                  {course.description}
                </Typography>
              </CardContent>
            </CardActionArea>
          </Card>
        ))}
      </div>
    </div>
  );
}

export default Courses;
