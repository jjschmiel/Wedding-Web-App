$(document).ready(function () {

    $(".RSVPButton").click(function () {
        var n = $(".AdditionalRSVP").length;
    });

    $("#AddRSVP").click(function () {
        var n = $(".AdditionalRSVP").length;
        $("#AdditionalRSVPHolder").append(
            '<div class="form-group AdditionalRSVP"><div class="col-md-2"></div><div class="col-md-4"><div class="panel panel-default"><div class="panel-heading">Additional RSVP<button type="button" class="DeleteRSVP glyphicon glyphicon-remove pull-right"></button></div><div class="panel-body"><div class="form-group"><label class="control-label col-md-4">First Name</label><div class="col-md-8"><input type="text" class="form-control AdditionalRSVPInputFirstName" name="additionalRSVP[' + n + '].FirstName" placeholder="Required" required /></div></div><div class="form-group"><label class="control-label col-md-4">Last Name</label><div class="col-md-8"><input type="text" class="form-control AdditionalRSVPInputLastName" name="additionalRSVP[' + n + '].LastName" placeholder="Required" required /></div></div><div class="form-group"><label class="control-label col-md-4">Email</label><div class="col-md-8"><input type="text" class="form-control AdditionalRSVPInputEmail" name="additionalRSVP[' + n + '].Email" /></div></div><div class="form-group"><label class="control-label col-md-4">Response </label><div class="col-md-8"><input type="radio" class="AdditionalRSVPInputResponse" name="additionalRSVP[' + n + '].Response" value="I' + "'" + 'll be there!  Can' + "'" + 't wait!" required /> I' + "'" + 'll be there!  Can' + "'" + 't wait!<br /><input type="radio" class="AdditionalRSVPInputResponse" name="additionalRSVP[' + n + '].Response" value="Unfortunately, I can' + "'" + 't make it." required /> Unfortunately, I can' + "'" + 't make it.</div></div></div></div></div></div>'
        );
    });

    $("body").on('click', '.DeleteRSVP', function () {
        $(this).parent().parent().parent().parent().remove();
        var x = 0;
        $(".AdditionalRSVP").each(function (x) {
            console.log(x);
            $(this).find('.AdditionalRSVPInput').attr('value', x);
            $(this).find('.AdditionalRSVPInputFirstName').attr('name', 'additionalRSVP[' + x + '].FirstName');
            $(this).find('.AdditionalRSVPInputLastName').attr('name', 'additionalRSVP[' + x + '].LastName');
            $(this).find('.AdditionalRSVPInputEmail').attr('name', 'additionalRSVP[' + x + '].Email');
            $(this).find('.AdditionalRSVPInputResponse').attr('name', 'additionalRSVP[' + x + '].Response');
        });

        $(this).attr('value', x);
        x++;
    });
});
    
