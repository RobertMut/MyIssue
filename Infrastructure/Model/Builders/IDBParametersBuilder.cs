﻿namespace MyIssue.Infrastructure.Model.Builders
{
    public interface IDBParametersBuilder
    {
        IDBParametersBuilder SetDBAddress(string address);
        IDBParametersBuilder SetUsername(string username);
        IDBParametersBuilder SetPassword(string pass);
        IDBParametersBuilder SetDatabase(string databaseName);
        DBParametersTemplate Build();
    }
}