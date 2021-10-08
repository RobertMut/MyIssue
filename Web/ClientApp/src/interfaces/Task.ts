interface IPagedTaskRequest {
  link: string,
  page: number,
  size: number,
}
interface IPagedResponse {
  pageNumber: number,
  pageSize: number,
  firstPage: string,
  lastPage: string,
  totalPages: number,
  totalRecords: number
  nextPage: string,
  previousPage: string,
  data: ITask[]
}
interface ITaskRoot {
  tasks: ITask[];
}
interface ITask {
  taskId: string,
  taskTitle: string,
  taskDescription: string,
  taskClient: string,
  taskAssignment: string,
  taskOwner: string,
  taskType: string,
  taskStart: string,
  taskEnd: string,
  taskCreationDate: string,
  createdByMail: string,
  employeeDescription: string,
}
export {
  IPagedResponse,
  IPagedTaskRequest,
  ITaskRoot,
  ITask
  }
