drop table donations.main cascade;
drop table widgets.donations cascade; 
drop table users.main cascade;
 
create table users.main (
	"user_id" serial primary key not null,
	"display_name" varchar(16) not null,
	"username" varchar(16) not null,
	"email" varchar(100) not null,
	"password" varchar(32) not null,
	"avatar" varchar(100) null,
	"exp" int not null default 0,
	"balance.current" float not null default 0,
	"balance.in_process" float not null default 0,
	"creation_date" timestamp not null default now(),
	"premium_expire" timestamp not null default timestamp '03.24.1996 00:00',
	"type" smallint not null default 0,
	"about" varchar(200) null,
	"twitch_username" varchar(50) null,
	"vk_id" int null,
	"youtube_channel" varchar(50) null
);

create table widgets.donations (
	"widg_id" serial primary key not null,
	"owner_id" int references "users.main" ("user_id"),
	"key" varchar(64) not null,
	"header_style" varchar(400) not null default 'font: 20px "PT Sans Bold"; color: darkorange; display: block; text-align: center',
	"header_pattern" varchar(200) not null default '{username} пожертвовал {amount}',
	"text_style" varchar(400) not null default 'font: 15px "PT Sans"; color: white; display: block; text-align: center',
	"image" varchar(100) null,
	"image_style" varchar(400) not null default 'width: 100px; margin: auto'
);
	
create table donations.main (
	don_id serial primary key not null,
	owner_id int references users.main (user_id) null,
	display_name varchar(32) null,
	value float not null,
	date timestamp not null default now(),
	message varchar(400) null
)