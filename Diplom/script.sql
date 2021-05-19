create table item
(
    id    bigserial not null
        constraint item_pk
            primary key,
    title varchar   not null
);

alter table item
    owner to postgres;

create unique index item_id_uindex
    on item (id);

create table item_item
(
    id             bigserial not null
        constraint item_item_pk
            primary key,
    parent_item_id bigint
        constraint item_item_item_id_fk
            references item
            on update cascade on delete cascade,
    child_item_id  bigint
        constraint item_item_item_id_fk_2
            references item
            on update cascade on delete cascade,
    count          integer
);

alter table item_item
    owner to postgres;

create table measure
(
    id    bigint  not null
        constraint measure_pk
            primary key,
    title varchar not null
);

alter table measure
    owner to postgres;

create table workshop
(
    id      serial not null
        constraint workshop_pk
            primary key,
    "Title" varchar
);

alter table workshop
    owner to postgres;

create table resource
(
    id             bigserial not null
        constraint resource_pk
            primary key,
    title          varchar   not null,
    count_on_store integer default 0,
    "measureId"    bigint
        constraint resource_measure_id_fk
            references measure
            on update cascade on delete cascade,
    "workshopId"   integer
        constraint resource_workshop_id_fk
            references workshop
            on update cascade on delete cascade
);

alter table resource
    owner to postgres;

create unique index resource_id_uindex
    on resource (id);

create table item_resource
(
    id          bigserial not null
        constraint partition_resource_pk
            primary key,
    item_id     bigint    not null
        constraint item_resource_item_id_fk
            references item
            on update cascade on delete cascade,
    resource_id bigint    not null
        constraint partition_resource_resource_id_fk
            references resource
            on update cascade on delete cascade,
    count       integer   not null
);

alter table item_resource
    owner to postgres;

create unique index partition_resource_id_uindex
    on item_resource (id);

