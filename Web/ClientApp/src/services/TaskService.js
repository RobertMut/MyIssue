"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.TaskService = void 0;
var core_1 = require("@angular/core");
var TaskService = /** @class */ (function () {
    function TaskService(httpclient, baseUrl) {
        this.httpclient = httpclient;
        this.baseUrl = baseUrl;
    }
    TaskService.prototype.getLastTasks = function (howMany, whose, headers) {
        return this.httpclient
            .get(this.baseUrl + 'Tasks/someOpen/' + whose + '/' + howMany, { headers: headers, responseType: 'text' });
    };
    TaskService.prototype.getTaskById = function (id, headers) {
        return this.httpclient
            .get(this.baseUrl + 'Tasks/' + id, { headers: headers, responseType: 'text' });
    };
    TaskService.prototype.getPagedFirst = function (pageNumber, pageSize, headers) {
        var pagedRequest = {
            link: null,
            page: pageNumber,
            size: pageSize
        };
        return this.httpclient
            .post(this.baseUrl + 'Tasks/pagedFirst', JSON.stringify(pagedRequest), { headers: headers, responseType: 'text' });
    };
    TaskService.prototype.getPagedLink = function (pageLink, headers) {
        var pagedRequest = {
            link: pageLink,
            page: null,
            size: null
        };
        return this.httpclient
            .post(this.baseUrl + 'Tasks/pagedLink', JSON.stringify(pagedRequest), { headers: headers, responseType: 'text' });
    };
    TaskService.prototype.createTask = function (task, headers) {
        return this.httpclient.post(this.baseUrl + 'Tasks', JSON.stringify(task), { headers: headers });
    };
    TaskService.prototype.updateTask = function (inputTask, headers) {
        return this.httpclient.put(this.baseUrl + 'Tasks/' + inputTask.TaskId, JSON.stringify(inputTask), { headers: headers });
    };
    TaskService = __decorate([
        core_1.Injectable({
            providedIn: 'root'
        }),
        __param(1, core_1.Inject('BASE_URL'))
    ], TaskService);
    return TaskService;
}());
exports.TaskService = TaskService;
//# sourceMappingURL=TaskService.js.map