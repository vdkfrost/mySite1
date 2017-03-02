delete from donations.main;
delete from users.main;

insert into users.main ("display_name", "username", "email", "password", "avatar", "exp", "balance.current", "balance.in_process", "creation_date", "premium_expire", "type", "about", "twitch_username", "vk_id", "youtube_channel")
values ('Wolfram', 'wolfram', 'admin@streamerspace.ru', 'ILoveTyumen72', null, 255804, 400, 82.53, now(), '03.24.2080', 100, 'Hello! I am bot Wolfram.', 'vdkfrost', 381169851, null);


insert into donations.main (owner_id, display_name, value, date, message)
values ((select user_id from users.main where username = 'wolfram'), (select display_name from users.main where username = 'wolfram'), 150, now(), 'Привет!');

select * from "users.main";
select * from donations.main;

select sum(value) from donations.main where owner_id = 3