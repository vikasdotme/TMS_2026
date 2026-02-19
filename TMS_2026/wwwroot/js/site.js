function openTaskModal(id, mode) {

    $('#taskForm')[0].reset();
    $('#taskForm input,textarea,select').prop('disabled', false);
    $('#saveBtn').show();

    if (mode === 'create') {
        $('#modalTitle').text('Create Task');
        $('#TaskId').val(0);
    }
    else {
        $.get('/Home/GetTaskById', { id }, function (task) {

            $('#TaskId').val(task.taskId);
            $('#TaskTitle').val(task.taskTitle);
            $('#TaskDescription').val(task.taskDescription);
            $('#TaskStatus').val(task.taskStatus);
            $('#TaskDueDate').val(task.taskDueDate?.substring(0, 10));
            $('#TaskRemarks').val(task.taskRemarks);

            if (mode === 'view') {
                $('#modalTitle').text('View Task');
                $('#taskForm input,textarea,select').prop('disabled', true);
                $('#saveBtn').hide();
            }

            if (mode === 'edit') {
                $('#modalTitle').text('Edit Task');
            }
        });
    }

    new bootstrap.Modal('#openTaskModal').show();
}
function openTaskModal(id, mode) {

    $('#taskForm')[0].reset();
    $('#taskForm input,textarea,select').prop('disabled', false);
    $('#saveBtn').show();

    if (mode === 'create') {
        $('#modalTitle').text('Create Task');
        $('#TaskId').val(0);
    }
    else {
        $.get('/Home/GetTaskById', { id }, function (task) {

            $('#TaskId').val(task.taskId);
            $('#TaskTitle').val(task.taskTitle);
            $('#TaskDescription').val(task.taskDescription);
            $('#TaskStatus').val(task.taskStatus);
            $('#TaskDueDate').val(task.taskDueDate?.substring(0, 10));
            $('#TaskRemarks').val(task.taskRemarks);

            if (mode === 'view') {
                $('#modalTitle').text('View Task');
                $('#taskForm input,textarea,select').prop('disabled', true);
                $('#saveBtn').hide();
            }

            if (mode === 'edit') {
                $('#modalTitle').text('Edit Task');
            }
        });
    }

    new bootstrap.Modal('#openTaskModal').show();
}


 
