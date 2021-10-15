class PagedTaskRequest {
  link: string;
  page: number;
  size: number;
}
class PagedResponse {
  pageNumber: number;
  pageSize: number;
  firstPage: string;
  lastPage: string;
  totalPages: number;
  otalRecords: number;
  nextPage: string;
  previousPage: string;
  data:Task[];
}
class TaskRoot {
  tasks: Task[];
}
class Task {
  taskId: string;
  taskTitle: string;
  taskDescription: string;
  taskClient: string;
  taskAssignment: string;
  taskOwner: string;
  taskType: string;
  taskStart: string;
  taskEnd: string;
  taskCreationDate: string;
  createdByMail: string;
  employeeDescription: string;
}
export {
  PagedResponse,
  PagedTaskRequest,
  TaskRoot,
  Task
  }
