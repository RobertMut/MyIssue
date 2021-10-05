export interface IPagedTaskRequest {
  link: string,
  page: number,
  size: number,
}
export interface IPagedResponse {
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
export interface ITaskroot {
  tasks: ITask[];
}
export interface ITask {
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
