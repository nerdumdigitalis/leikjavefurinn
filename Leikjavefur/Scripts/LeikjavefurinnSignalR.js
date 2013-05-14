$(document).ready(function () {

    var hub = $.connection.communication;
    var myTurn = false;
    var grid = [['', '', ''], ['', '', ''], ['', '', '']];
    var myPosition = 1;
    var gameGroup = '';
    var chatGroup = '';
    var move = 1;
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
    hub.client.receiveRollValueAndNextPlayer = function (_oldPosition, _newPosition, _player, _isGameOver, roll, snakeOrLadder) {

        var oldPosition = 0;
        oldPosition = parseInt(_oldPosition);
        var newPosition = 0;
        newPosition = parseInt(_newPosition);
        var player = 0;
        player = parseInt(_player);
        var isGameOver = "";
        isGameOver = _isGameOver;

        //Start by moving player to new position
        $("#rolltext").text("Player " + player + " rolls a " + roll);
        var difference = newPosition - oldPosition;
        if (snakeOrLadder == "true")
        {
            for (var i = 1; i <= roll; i++) {
                var nextPos = $("#" + (oldPosition + i)).position();
                $("#player" + player).animate({ 'top': nextPos.top + 'px', 'left': nextPos.left + 'px' }, 300, function () { });
            }
            var endPos = $("#" + (newPosition)).position();
            $("#player" + player).animate({ 'top': endPos.top + 'px', 'left': endPos.left + 'px' }, 1000, function () { });
        }
        else if(difference > 0)
        {
            for(var i = oldPosition++; i <= newPosition; i++)
            {
                var nextPos = $("#" + i).position();
                $("#player" + player).animate({ 'top': nextPos.top + 'px', 'left': nextPos.left + 'px' }, 300, function () { });
            }
        }

        //find out if i should do next
        var nextPlayer = player;
        if (nextPlayer == 4){
            nextPlayer = 1;
        }
        else{
            nextPlayer++;
        }

        var myNumber = parseInt(getMyPlayerNumber());
        if (nextPlayer == myNumber) {
            if (isGameOver != "true") {
                myTurn = true;
                $("#dice").show();
            }
        }
        else if (player == myNumber) {
            myPosition = newPosition;
        }

        if (isGameOver == "true" && player == myNumber)
        {
            hub.server.gameOver(gameGroup);
            alert("You won! Congratulationz");
        }
        else if (isGameOver == "true" && player != myNumber)
            alert("You lost. Sorry friend :(");
    };

    //Connect
    $.connection.hub.start().done(function () {

        //Get Groups
        if (typeof (getGameGroup) === 'function') {
            gameGroup = getGameGroup();
            myTurn = getMyTurn();
        }
        if (typeof (getChatGroup) === 'function')
            chatGroup = getChatGroup();

        if (typeof (getMyPlayerNumber) === 'function') {
            if(getMyPlayerNumber() == true)
                $("#dice").show();
        };

        if (gameGroup != ''){
            hub.server.join(gameGroup);
            chatGroup = gameGroup;
        }
        else if (chatGroup != '')
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
                    hub.server.gameOver(gameGroup);
                    alert("You won!!, play again!");
                }
            }
        });

        //Snakes'N'Ladders Send Functions
        $("#dice").click(function () {
            if (myTurn == true) {
                myTurn = false;
                hub.server.rollDice(gameGroup, getMyPlayerNumber(), myPosition);
                $("#dice").hide();
            }
        });

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
})