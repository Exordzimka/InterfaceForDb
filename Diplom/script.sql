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

create table partition
(
    id    bigserial not null
        constraint partition_pk
            primary key,
    title varchar   not null
);

alter table partition
    owner to postgres;

create unique index partition_id_uindex
    on partition (id);

create table item_partition
(
    id           bigserial not null
        constraint item_partition_pk
            primary key,
    item_id      bigint    not null
        constraint item_partition_item_id_fk
            references item
            on update cascade on delete cascade,
    partition_id bigint    not null
        constraint item_partition_partition_id_fk
            references partition
            on update cascade on delete cascade
);

alter table item_partition
    owner to postgres;

create unique index item_partition_id_uindex
    on item_partition (id);

create table resource
(
    id           bigserial not null
        constraint resource_pk
            primary key,
    title        varchar   not null,
    partition_id bigint    not null
        constraint resource_partition_id_fk
            references partition
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


