function sendTeamId(){
    if ($('#teamIdInput').val()) {
        $('#chooseSection').html("");
        $.ajax({
            method: 'POST',
            url: 'LotteryNumberList',
            xhrFields: {
                withCredentials: true
            },
            data:{ teamId: $('#teamIdInput').val()},
            success: function(response){
                if (response.info) {
                    $('#teamIdInput').attr('disabled', true);
                    $('#teamIdButton').attr('disabled', true);
                    for (var i = 1; i <= 48; i++) {
                        if ($.inArray(i, response.numlist) >= 0) {
                            $('#chooseSection').append('<label for="'+i+'">'+i+'</label><input id="'+i+'" type="radio" name="lottery" value="'+i+'" disabled>');
                        } else {
                            $('#chooseSection').append('<label for="'+i+'">'+i+'</label><input id="'+i+'" type="radio" name="lottery" value="'+i+'">');
                        }
                    }
                    $('#chooseSection').append('<button id="sendLotteryButton" onclick="sendLotteryNumber()">送出</button>');
                }
                else {
                    $.notifyDefaults({
                        type: 'danger',
                        allow_dismiss: false
                    });
                    $.notify('輸入錯誤，請從新查詢');
                }
            }
        });
    }
    else {
        $.notifyDefaults({
            type: 'danger',
            allow_dismiss: false
        });
        $.notify('請輸入你的小隊代碼！');
    }
}

function sendLotteryNumber() {
    var number = $('#chooseSection').find('input:checked').val();
    if (number == null) {
        $.notifyDefaults({
            type: 'danger',
            allow_dismiss: false
        });
        $.notify('請選擇一個號碼！');
    }
    else {
        $('#sendLotteryButton').attr('disabled', true);
        $.ajax({
            method: 'GET',
            url: 'CheckTime',
            xhrFields: {
                withCredentials: true
            },
            success: function(response){
                if (response.info) {
                    $.ajax({
                        method: 'POST',
                        url: 'ChooseNumber',
                        xhrFields: {
                            withCredentials: true
                        },
                        data:{ teamId: $('#teamIdInput').val(), number: number},
                        success: function(response){
                            if (response.info) {
                                $.notifyDefaults({
                                    type: 'success',
                                    allow_dismiss: false
                                });
                                $.notify('操作成功！');
                            }
                            else {
                                $.notifyDefaults({
                                    type: 'danger',
                                    allow_dismiss: false
                                });
                                $.notify('發生錯誤，請重新操作');
                                $('#sendLotteryButton').attr('disabled', false);
                            }
                        }
                    });
                }
                else {
                    $.notifyDefaults({
                        type: 'danger',
                        allow_dismiss: false
                    });
                    $.notify('尚未開放大樂透，請關注Line@相關資訊');
                }
            }
        });
    }
}
