function ClearText()
{
    $('input[type=text]').each(function ()
    {
        $(this).val('');
    });
}

function Validate()
{
    var Name = $("#name").val();
    if (Name != "")
    {
        return true;
    }
    else
    {
        //alert('Please enter name for Course');
        $("#ErrorAlert").show().fadeOut(3500);
        return false;
    }
}

function AddRecord()
{
    var empObj =
       {
           Name: $("#name").val(),
       };
    //Using Ajax request call /Home/Add method and insert data in Db
    $.ajax({
        type: 'Post',
        url: '/Course/Create',
        data: JSON.stringify(empObj),
        contentType: "application/json; charset=utf-8",
        success: function ()
        {
            //alert("Course details added successfully");
            $("#SuccessAlert").show();
            window.location.href = "/Course/Index";
        },
        error: function ()
        {
            //alert("Some error occured while adding record.");
            $('#myModal').modal('hide');
        }
    });
}

function LoadRecords()
{
    $.ajax({
        type: 'Get',
        url: '/Course/Index',
        contentType: "application/json; charset=utf-8",
        success: function ()
        {
            $('#myModal').modal('hide');
        },
        error: function ()
        {
            $('#myModal').modal('hide');
        }
    });
}

function EditCourse(id)
{
    alert(id);
    //Get the details of the Course from selected Course name
    //$.ajax({
    //    type: 'Get',
    //    url: '/Course/GetCourse',
    //    contentType: "application/json; charset=utf-8",
    //    success: function ()
    //    {
    //        $('#myModal').modal('show');
    //    },
    //    error: function () {
    //        $('#myModal').modal('hide');
    //    }
    //});
}

$("#btnAdd").click(function ()
{
    if (Validate())
    {
        AddRecord();
    }
});

$("#btnClear").click(function ()
{
    ClearText();
});