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
