$(function(){

});

function sendTeamId(){
    if ($('#teamIdInput').val()) {
        $.ajax({
            method: 'POST',
            url: 'LotteryNumberList',
            data:{ teamId: $('#teamIdInput').val()},
            success: function(response){
                if (response.info) {
                    $('#teamIdInput').attr('dissabled', true);
                    console.log(response.numlist);
                }
            }
        });
    }
}