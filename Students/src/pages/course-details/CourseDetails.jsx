import "./course-details.css";
import { useGet } from "../../hooks/useGet";
import Loader from "../../components/loader/Loader";

function CourseDetails() {
  const courseId = window.location.pathname.split("/")[2];

  const { data: course, isLoading } = useGet(
    ["Course", courseId],
    `/api/course/${courseId}`
  );

  if (isLoading) return <Loader />;

  const { result } = course;
  return (
    <div className="course-details">
      <h1>{result.title}</h1>
      <img
        className="img-fluid"
        width={550}
        src={result.imageUrl}
        alt={result.title}
      />
      <p>{result.price} $</p>
      <p>{result.faculty.name}</p>
      <p>{result.description}</p>
    </div>
  );
}

export default CourseDetails;
