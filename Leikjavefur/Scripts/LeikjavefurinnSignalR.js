var hub = $.connection.communication;
var myTurn = false;
var grid = [['', '', ''], ['', '', ''], ['', '', '']];
var gameGroup = '';
var chatGroup = '';
var textArea = document.getElementById('ChatArea');

//Chat Receive Functions
hub.client.receiveMessage = function (message) {
    var text = document.createTextNode(message + '\n');
    textArea.appendChild(text);
    textArea.scrollTop = textArea.scrollHeight;
};

//TicTacToe Receive Functions
hub.client.cellClicked = function (cellId) {
    var hisMark = getHisMark();        
    addToGrid(cellId, hisMark);
    $("#" + cellId).text(hisMark);

    var isGameOver = checkIfGameOver();

    if (isGameOver == true) {
        alert("You lost, play again!");
    }
    else {
        myTurn = true;
    }
};

//Snakes'N'Ladders Receive Functions

//Connect
$.connection.hub.start().done(function () {
    debugger;

    //Get Groups
    if (typeof (getGameGroup) === 'function') {
        gameGroup = getGameGroup();
        myTurn = getMyTurn();
    }
    if (typeof (getChatGroup) === 'function')
        chatGroup = getChatGroup();

    /*if (gameGroup != ''){
        hub.server.join(gameGroup);
        chatGroup = gameGroup;
    }
    else if (chatGroup != '')
        hub.server.join(chatGroup);*/

    if (gameGroup != '') {
        hub.server.join(gameGroup);
    }
    if (chatGroup != '')
        hub.server.join(chatGroup);

    //Chat Send Functions
    $("#SendButton").click(function () {
        var userName = getUserName();       
        if (userName != '') {
            var message = $('#TextBoxMessage').val();
            if (message != '') {
                message = userName + ": ";
                message += $('#TextBoxMessage').val();
                hub.server.sendMessage(chatGroup, message);
                $('#TextBoxMessage').val('');
                var text = document.createTextNode(message + '\r');
                textArea.appendChild(text);
                textArea.scrollTop = textArea.scrollHeight;
                document.getElementById("TextBoxMessage").focus();
            }
        }
        else {
            $('#TextBoxMessage').val('');
        }
    });

    //TicTacToe Send Functions

    //$("#tictactoe tr td").click(function () {
    //    var hisMark = getHisMark();
    //    var myMark = getMyMark();
    //    if (myTurn == true && $(this).text() != hisMark && $(this).text() != myMark) {
    //        myTurn = false;
    //        addToGrid(this.id, myMark);
    //        hub.server.clickCell(gameGroup, this.id);
    //        $(this).text(myMark);
    //        var isGameOver = checkIfGameOver();
    //        if (isGameOver == true) {
    //            alert("You won!!, play again!");
    //        }
    //    }
    //});

           //  TEST #2
    $("#tictactoe tr td").click(function (event) {
        var elementId = event.target.id;
        var hisMark = getHisMark();
        var myMark = getMyMark();

        if (myTurn == true && $("#" + elementId).text() != hisMark && $("#" + elementId).text() != myMark) {
            myTurn = false;
            addToGrid(elementId, myMark);
            hub.server.clickCell(gameGroup, elementId);
            $("#" + elementId).text(myMark);
            var isGameOver = checkIfGameOver();
            if (isGameOver == true) {
                alert("You won!!, play again!");
            }
        }
    });




    //Snakes'N'Ladders Send Functions

});

function addToGrid(id, mark) {
    if (id == 1)
        grid[0][0] = mark;
    else if (id == 2)
        grid[0][1] = mark;
    else if (id == 3)
        grid[0][2] = mark;
    else if (id == 4)
        grid[1][0] = mark;
    else if (id == 5)
        grid[1][1] = mark;
    else if (id == 6)
        grid[1][2] = mark;
    else if (id == 7)
        grid[2][0] = mark;
    else if (id == 8)
        grid[2][1] = mark;
    else if (id == 9)
        grid[2][2] = mark;
}

function checkIfGameOver() {
    if (grid[0][0] == grid[0][1] && grid[0][0] == grid[0][2] && grid[0][0] != '')
        return true;
    else if (grid[1][0] == grid[1][1] && grid[1][0] == grid[1][2] && grid[1][0] != '')
        return true;
    else if (grid[2][0] == grid[2][1] && grid[2][0] == grid[2][2] && grid[2][0] != '')
        return true;
    else if (grid[0][0] == grid[1][0] && grid[0][0] == grid[2][0] && grid[0][0] != '')
        return true;
    else if (grid[0][1] == grid[1][1] && grid[0][1] == grid[2][1] && grid[0][1] != '')
        return true;
    else if (grid[0][2] == grid[1][2] && grid[0][2] == grid[2][2] && grid[0][2] != '')
        return true;
    else if (grid[0][0] == grid[1][1] && grid[0][0] == grid[2][2] && grid[0][0] != '')
        return true;
    else if (grid[0][2] == grid[1][1] && grid[0][2] == grid[2][0] && grid[0][2] != '')
        return true;

    return false;
}