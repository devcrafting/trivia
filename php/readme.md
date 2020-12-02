# PHP setup

Basic kata contains only `Game.php` and `GameRunner.php` files.

If you have a PHP 7.4 env (or compatible), you can probably use it directly.

Else you can use Docker based setup here (using Docker-Compose [as proposed here](https://thephp.website/en/issue/php-docker-quick-setup/)):

- use `docker-compose run php src/Runner.php` to run the Trivia console app
- use `docker-compose run phpunit` to run PHPUnit tests
- use `docker-compose run composer [args]` to run composer with your args
