<?php
    include_once "includes/functions.php";

    if (isset($_GET['eventName'])){
        $isRegistered = eventRegistered( $_GET['eventName']);
        //$event_name = str_replace("'", "\'", $_GET['eventName']);
        setcookie('eventName', $_GET['eventName'], time() + (86400 * 3), "/");
        if ($isRegistered == 1) {
            echo "<script>alert('You already have submitted the guests for this event!')</script>";
            echo "<script>window.location.href='https://event-venue.website/index.php'</script>";
        }
    }
?>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Add Guests | Event Management Online Booking</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <link rel="shortcut icon" href="pictures/Nicolas_Logo.jpg" type="image/x-icon">
    <link rel="stylesheet" href="css/guest.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.0/jquery.min.js"></script>
    <script src="js/guest.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.1.js"></script>
</head>

<body>
    <div class="header">
        <div class="logo">
            <a href="index.php"><span class="link"></span></a>
            <img src="pictures/Nicolas_Logo.jpg" alt="">
            <span>Event Management Online Booking</span>
        </div>

    </div>
    <br>
    <br>
    <br>
    <br>

    <div class="search-text">Insert the data needed from your guests</div>


    <div class="center-search">
        <div class="search-events">
            <div class="search-event">
                <!--<div class="search">
                    <form action="" onsubmit="return false">
                        <input type="text" name="search" id="searchEventSearch" placeholder="Search for a guest...">
                        <button type="submit" name="buyEvent"><i
                                class="fa fa-search"></i></button>
                        <span class="course">
                            <select name="type" id="course" required>
                                <option hidden disabled selected value>-- SEARCH BY --</option>
                                <option value="name">NAME</option>
                                <option value="price">ADDRESS</option>
                                <option value="place">EMAIL</option>
                                <option value="due">NUMBER</option>
                            </select>
                        </span>
                    </form>

                </div>-->
                    <div class="events-list">
                        <table class="events-table" id="events">
                            <thead>
                                <tr>
                                    <th>NAME</th>
                                    <th>ADDRESS</th>
                                    <th>EMAIL</th>
                                    <th>NUMBER</th>
                                    <th>ACTION</th>
                                </tr>
                                <tbody>
                                </tbody>

                            </thead>
                        </table>
                        <input id="submit-btn" name="uploadData" type="submit" value="SUBMIT FORM" class="submit-table-btn">
                    </div>
                

            </div>

        </div>
    </div>

    <div class="center-search">
        <div class="book-events">
            <div class="book-event">
                <div class="event-text">
                    <div class="slides">
                        <input type="radio" name="radio-btn" id="radio1">
                        <input type="radio" name="radio-btn" id="radio2">
                        <input type="radio" name="radio-btn" id="radio3">
                        <input type="radio" name="radio-btn" id="radio4">
                        <div class="slide first">
                            <img src="pictures/index/christening.jpg" alt="">
                        </div>
                        <div class="slide">
                            <img src="pictures/index/birthday.jpg" alt="">
                        </div>
                        <div class="slide">
                            <img src="pictures/index/debut.jpg" alt="">
                        </div>
                        <div class="slide">
                            <img src="pictures/index/wedding.jpg" alt="">
                        </div>
                        <div class="navigation-auto">
                            <div class="auto-btn1"></div>
                            <div class="auto-btn2"></div>
                            <div class="auto-btn3"></div>
                            <div class="auto-btn4"></div>
                        </div>
                    </div>
                    <div class="navigation-manual">
                        <label for="radio1" class="manual-btn"></label>
                        <label for="radio2" class="manual-btn"></label>
                        <label for="radio3" class="manual-btn"></label>
                        <label for="radio4" class="manual-btn"></label>
                    </div>
                </div>
                <div class="event-form">
                    <div id="freshmen" class="container">
                        <div class="title">Guest's Data</div>
                        <form method="post" enctype="multipart/form-data" onsubmit="return false">
                            <div class="user-details">

                                <div class="input-box">
                                    <span class="details">Full Name</span>
                                    <input id="fullname" type="text" name="Ffname" placeholder="Enter guest's full name" required>
                                </div>

                                <div class="input-box">
                                    <span class="details">Full Address</span>
                                    <input id="fulladdress" type="text" name="Flname" placeholder="Enter guest's full address" required>
                                </div>

                                <div class="input-box">
                                    <span class="details">E-mail</span>
                                    <input id="email" type="text" name="Fmname" placeholder="Enter guest's e-mail" required>
                                </div>

                                <div class="input-box">
                                    <span class="details">Number</span>
                                    <input id="number" type="text" name="Femail" placeholder="Enter guest's number" required>
                                </div>

                            </div>
                            <div class="button">
                                <input class="add-user" type="submit" value="Add to Guests" name="Fresh">
                            </div>

                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <footer>
        <div class="footer">
            <div class="like" style="text-align: center;">
                <p style="font-size: 18px;">Like what you see?</p>
                <br>
                <p>Then give us a call and we'll chat through what you need.</p>
                <p>We've got coffee, tea, and buscuits!</p>
                <p style="font-weight: 500">(if you're quick)</p>
            </div>
            <div class="contact">
                <img src="pictures/Nicolas_Logo.jpg" alt="">
                <span style="font-size: 18px;">CONTACT US</span>
                <p><i class="fa fa-comments"></i> (+63) 9366296799</p>
                <p><a href="https://www.imaqtchael@gmail.com" style="text-decoration: none; color: white;"><i
                            class="fa fa-envelope"></i> event_management@gmail.com</a></p>
                <p><i class="fa fa-home"></i> Nicolas Resort Building, Phase 1a Sub-Urban Village Brgy. San Jose, Rodriguez, Rizal, Philippines</p>
            </div>
            <div class="follow">
                <p style="font-size: 18px;">FOLLOW US</p>
                <br>
                <a href="https://www.facebook.com/" target="_blank" title="Visit our Facebook Page"><i
                        class="fa fa-facebook-square"></i></a>
                <a href="https://www.instagram.com/" target="_blank" title="Visit our Instagram account"><i
                        class="fa fa-instagram"></i></a>
                <a href="https://twitter.com/" target="_blank" title="Visis our Twitter account"><i
                        class="fa fa-twitter-square"></i></a>

            </div>
        </div>
        <br>
        <center>
            <p>@2022 QuickTech. All Rights Reserved.</p>
        </center>
    </footer>

</body>

</html>