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
  TaskId: string;
  TaskTitle: string;
  TaskDescription: string;
  TaskClient: string;
  TaskAssignment: string;
  TaskOwner: string;
  TaskType: string;
  TaskStart: string;
  TaskEnd: string;
  TaskCreationDate: string;
  CreatedByMail: string;
  EmployeeDescription: string;
}
export {
  PagedResponse,
  PagedTaskRequest,
  TaskRoot,
  Task
  }
