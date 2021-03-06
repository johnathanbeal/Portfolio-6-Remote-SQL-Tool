PGDMP     $                    x           rolodex    11.5    11.1     �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                       false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                       false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                       false            �           1262    16408    rolodex    DATABASE     y   CREATE DATABASE rolodex WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'en_US.UTF-8' LC_CTYPE = 'en_US.UTF-8';
    DROP DATABASE rolodex;
             postgres    false            �           0    0    SCHEMA public    ACL     �   REVOKE ALL ON SCHEMA public FROM rdsadmin;
REVOKE ALL ON SCHEMA public FROM PUBLIC;
GRANT ALL ON SCHEMA public TO postgres;
GRANT ALL ON SCHEMA public TO PUBLIC;
                  postgres    false    3            �            1259    16433    address    TABLE     �   CREATE TABLE public.address (
    aid integer NOT NULL,
    pid integer NOT NULL,
    address text NOT NULL,
    city text NOT NULL,
    state text,
    zip text,
    created_on date NOT NULL
);
    DROP TABLE public.address;
       public         postgres    false            �            1259    16431    address_aid_seq    SEQUENCE     �   CREATE SEQUENCE public.address_aid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 &   DROP SEQUENCE public.address_aid_seq;
       public       postgres    false    199            �           0    0    address_aid_seq    SEQUENCE OWNED BY     C   ALTER SEQUENCE public.address_aid_seq OWNED BY public.address.aid;
            public       postgres    false    198            �            1259    16422    people    TABLE     �   CREATE TABLE public.people (
    id integer NOT NULL,
    firstname text NOT NULL,
    lastname text NOT NULL,
    email text,
    created_date date NOT NULL
);
    DROP TABLE public.people;
       public         postgres    false            �            1259    16420    people_id_seq    SEQUENCE     �   CREATE SEQUENCE public.people_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 $   DROP SEQUENCE public.people_id_seq;
       public       postgres    false    197            �           0    0    people_id_seq    SEQUENCE OWNED BY     ?   ALTER SEQUENCE public.people_id_seq OWNED BY public.people.id;
            public       postgres    false    196            i           2604    16436    address aid    DEFAULT     j   ALTER TABLE ONLY public.address ALTER COLUMN aid SET DEFAULT nextval('public.address_aid_seq'::regclass);
 :   ALTER TABLE public.address ALTER COLUMN aid DROP DEFAULT;
       public       postgres    false    198    199    199            h           2604    16425 	   people id    DEFAULT     f   ALTER TABLE ONLY public.people ALTER COLUMN id SET DEFAULT nextval('public.people_id_seq'::regclass);
 8   ALTER TABLE public.people ALTER COLUMN id DROP DEFAULT;
       public       postgres    false    196    197    197            �          0    16433    address 
   TABLE DATA               R   COPY public.address (aid, pid, address, city, state, zip, created_on) FROM stdin;
    public       postgres    false    199   A       �          0    16422    people 
   TABLE DATA               N   COPY public.people (id, firstname, lastname, email, created_date) FROM stdin;
    public       postgres    false    197   �       �           0    0    address_aid_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.address_aid_seq', 276, true);
            public       postgres    false    198            �           0    0    people_id_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('public.people_id_seq', 589, true);
            public       postgres    false    196            m           2606    16441    address address_pkey 
   CONSTRAINT     S   ALTER TABLE ONLY public.address
    ADD CONSTRAINT address_pkey PRIMARY KEY (aid);
 >   ALTER TABLE ONLY public.address DROP CONSTRAINT address_pkey;
       public         postgres    false    199            k           2606    16430    people people_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.people
    ADD CONSTRAINT people_pkey PRIMARY KEY (id);
 <   ALTER TABLE ONLY public.people DROP CONSTRAINT people_pkey;
       public         postgres    false    197            n           2606    16442    address address_pid_fkey    FK CONSTRAINT     t   ALTER TABLE ONLY public.address
    ADD CONSTRAINT address_pid_fkey FOREIGN KEY (pid) REFERENCES public.people(id);
 B   ALTER TABLE ONLY public.address DROP CONSTRAINT address_pid_fkey;
       public       postgres    false    3691    197    199            �   5  x����J1���S�V&_�ͱU���JO^�څ4���I� �a��'a?B�d8p��z�(�c`k��*�)r�[�@�LY�(za;b�� (j�Ҡ���3y�|t�+~�x��b"ؔ-<n����րYlG�K%�ݻ��S v��3�nJ�)L��ɽ/�J��؂�s��@o�>Ŕ��z�5ˣX;�hzn:�Pӈ8�4"��!	5�HAM#*�nF�IP#��!���D��7�j�L#�j���_M�;���=�N`�ZM�Ք�!ZM�P�S���F7�R�j�3@�Bk盗���Iwg�      �   ?  x���M��0�ϓ��'���V[�Z��V�K/N�$��� K}ǁ Zm�V�K2�d��?&JH�Lro�N׈ֱ��(��Q��yAZX�j)4$B�U2/{�J�n�!�j�S�ۢm!o1����A�B꼦���]=���Т.�ôU�V	��^v���3������̠�,k���lz�E^	�Xj�s�x�`K����}2X��.{���[6��.m+��Ģ�sx@+��Qj�'����p֝p�������w�6�5v�W%2 CϞ��c�)t��t�5��6��JΧ���u)21V#l�f���و�~��	+��5�l��\�)<X<�c�2Z�R��dU��d:��u��r�sx�!�+�|��kp;TG�����F!�0f#a����~�^1��h�w̃�M�ae\n�!�N�I��<JE��ɲ�.dE�'�H�Rt�3k��TxsNp����H�i�\�������d���5r��v�~�^�y/?X?_�~��[�w�U�~��8�����_b1|74�j�i;�j!��߉t��oO��<��;噼O�_,�?>���     