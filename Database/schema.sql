create table audiences
(
    audience_id int auto_increment
        primary key,
    name        varchar(60)  null,
    code        varchar(100) not null,
    constraint unique_code
        unique (code)
);

create table hardware_statuses
(
    hardware_status_id int auto_increment
        primary key,
    name               varchar(50) not null,
    constraint unique_name
        unique (name)
);

create table roles
(
    role_id int auto_increment
        primary key,
    name    varchar(50) not null,
    constraint unique_name
        unique (name)
);

create table users
(
    user_id      int auto_increment
        primary key,
    login        varchar(50)  not null,
    password     varchar(60)  not null,
    role_id      int          not null,
    email        varchar(100) null,
    phone_number varchar(20)  null,
    first_name   varchar(50)  not null,
    second_name  varchar(50)  not null,
    patronymic   varchar(50)  not null,
    constraint users_roles_role_id_fk
        foreign key (role_id) references roles (role_id)
);

create table hardware
(
    hardware_id         int auto_increment
        primary key,
    name                varchar(500) not null,
    inventory_number    varchar(255) not null,
    image               longblob     null,
    price               double       null,
    responsible_user_id int          null,
    audience_id         int          null,
    status_id           int          null,
    constraint unique_inventory_number
        unique (inventory_number),
    constraint hardware_audiences_audience_id_fk
        foreign key (audience_id) references audiences (audience_id),
    constraint hardware_hardware_statuses_hardware_status_id_fk
        foreign key (status_id) references hardware_statuses (hardware_status_id),
    constraint hardware_users_user_id_fk
        foreign key (responsible_user_id) references users (user_id)
);

create table hardware_audiences_history
(
    hardware_audiences_history_id int auto_increment
        primary key,
    hardware_id                   int      not null,
    audience_id                   int      not null,
    transferred_date              datetime not null,
    removed_date                  datetime null,
    constraint hardware_audiences_history_audiences_audience_id_fk
        foreign key (audience_id) references audiences (audience_id),
    constraint hardware_audiences_history_hardware_hardware_id_fk
        foreign key (hardware_id) references hardware (hardware_id)
);

create table hardware_responsibility_history
(
    hardware_responsibility_history_id int auto_increment
        primary key,
    hardware_id                        int          not null,
    responsible_user_id                int          not null,
    comment                            varchar(255) null,
    responsibility_start_date          datetime     not null,
    responsibility_end_date            datetime     null,
    constraint hardware_responsibility_history_hardware_hardware_id_fk
        foreign key (hardware_id) references hardware (hardware_id),
    constraint hardware_responsibility_history_users_user_id_fk
        foreign key (responsible_user_id) references users (user_id)
);


