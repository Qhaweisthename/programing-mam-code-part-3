/*Preload for POLICING SA 
'Please run this query using SQL Developer or SQL*Plus'*/ 
Create table OFFICER
(
  officer_ID            number(5)      not null    primary key,
  officer_fname         varchar2(100)  not null,
  officer_sname         varchar2(100)  not null,
  officer_contact       varchar2(20)  not null
);

Create table CITIZEN
(
  citizen_id            varchar2(5)    not null    primary key,
  citizen_fname         varchar2(100)  not null,
  citizen_sname         varchar2(100)  not null,
  citizen_address       varchar2(200)   not null,
  citizen_contact       varchar2(12)  not null
);

Create table CRIMINAL
(
  criminal_ID           varchar2(13)      not null    primary key,
  criminal_fname        varchar2(100)  not null,
  criminal_sname        varchar2(100)   null,
  criminal_dob          date           null
);

Create table OFFENCE
(
  offence_id          varchar2(10)    not null    primary key,
  offence_name        varchar2(20)   not null,
  offence_sentence    varchar2(20)   not null,
  offence_rating      number(3)       not null
);

Create table CASES
(
  case_id        varchar2(10)    not null    primary key,
  case_date      date            not null,
  officer_id     number(5)       not null constraint fk_officer_id
                 references OFFICER(officer_id),
  citizen_id     varchar2(5)       not null constraint fk_citizen_id
                 references CITIZEN(citizen_id),
  criminal_ID    varchar2(13)       not null constraint fk_criminal_id
                 references CRIMINAL(criminal_ID),
  offence_id     varchar2(10)       not null constraint fk_offence_id
                 references OFFENCE(offence_id)
);

insert all
   into OFFICER(officer_ID, officer_fname, officer_sname, officer_contact)
    values(101, 'Cameron', 'Willis', '0843569851')
   into OFFICER(officer_ID, officer_fname, officer_sname, officer_contact)
    values(102, 'Jess', 'Wait', '0763698521')
   into OFFICER(officer_ID, officer_fname, officer_sname, officer_contact)
    values(103, 'Alex', 'Gumede', '0786598521')
   into OFFICER(officer_ID, officer_fname, officer_sname, officer_contact)
     values(104, 'Bob', 'Du Preez', '0796369857')
   into OFFICER(officer_ID, officer_fname, officer_sname, officer_contact)
   values(105, 'Simon', 'Jones', '0826598741')
  Select * from dual;
  Commit;
  
  
  insert all
  into CITIZEN(citizen_id, citizen_fname, citizen_sname, citizen_address, citizen_contact)
    values('A1001', 'Wayne', 'Bitterhout', '15 Table rd', '0769856895')
  into CITIZEN(citizen_id, citizen_fname, citizen_sname, citizen_address, citizen_contact)
   values('A1002', 'Jeff', 'James',  '28 Sea Side rd', '0742598657')
  into CITIZEN(citizen_id, citizen_fname, citizen_sname, citizen_address, citizen_contact)
    values('A1003', 'Jabu', 'Bloggs', '19 Upper End', '0863256982')
  into CITIZEN(citizen_id, citizen_fname, citizen_sname, citizen_address, citizen_contact)
     values('A1004', 'Clark', 'Smith', '27 South end', '0785659857')
  into CITIZEN(citizen_id, citizen_fname, citizen_sname, citizen_address, citizen_contact) 
   values('A1005', 'Xolani', 'Pasela', '12 Main rd', '0712369571')
  into CITIZEN(citizen_id, citizen_fname, citizen_sname, citizen_address, citizen_contact) 
   values('A1006', 'Quma', 'Zola', '88 Main rd', '0732369551')
  into CITIZEN(citizen_id, citizen_fname, citizen_sname, citizen_address, citizen_contact) 
   values('A1007', 'Denish', 'Panisa', '12 Cape rd', '0812368578')
  Select * from dual;
  Commit;
  
  insert all
  into CRIMINAL(criminal_ID, criminal_fname, criminal_sname, criminal_dob)
    values('crim101', 'Sam', 'Jackson', '1 November 1979')
  into CRIMINAL(criminal_ID, criminal_fname, criminal_sname, criminal_dob)
    values('crim102', 'Henry', 'Willis', '11 October 1973')
  into CRIMINAL(criminal_ID, criminal_fname, criminal_sname, criminal_dob)
     values('crim103', 'Bruce', 'McKenzie', '2 November 1971')
  into CRIMINAL(criminal_ID, criminal_fname, criminal_sname, criminal_dob)
      values('crim104', 'Alex', 'Conradie', '1 January 1990')
  into CRIMINAL(criminal_ID, criminal_fname, criminal_sname, criminal_dob)
      values('crim105', 'Luvuyo', 'Zolani', '2 June 1982')
  into CRIMINAL(criminal_ID, criminal_fname, criminal_sname, criminal_dob)
      values('crim106', 'Archie', 'Klein', '12 August 1975')
  into CRIMINAL(criminal_ID, criminal_fname, criminal_sname, criminal_dob)
      values('crim107', 'Fabian', 'Willemse', '2 November 1969')
  into CRIMINAL(criminal_ID, criminal_fname, criminal_sname, criminal_dob)
      values('crim108', 'Thomas', 'Abrahams', '22 November 1980')
  Select * from dual;
  
  insert all
  into OFFENCE(offence_id, offence_name, offence_sentence, offence_rating)
    values('33311', 'House Robbery',  '2 years', 2)
   into OFFENCE(offence_id, offence_name, offence_sentence, offence_rating)
    values('33312', 'Hi Jacking', '7 years', 3)
   into OFFENCE(offence_id, offence_name, offence_sentence, offence_rating)
     values('33313', 'Drug Possesion', '12 years', 8)
   into OFFENCE(offence_id, offence_name, offence_sentence, offence_rating)
      values('33314', 'Attempted Murder',  '30 years', 9)
  into OFFENCE(offence_id, offence_name, offence_sentence, offence_rating)
   values('33315', 'Tax Fraud', '20 years', 7)
  Select * from dual;
  Commit;
  
 insert all
  into CASES(case_id, case_date, officer_id, citizen_id, criminal_ID, offence_id)
    values(10101,	'15 October 2017',	102,	'A1001', 'crim101',	'33311')
  into CASES(case_id, case_date, officer_id, citizen_id, criminal_ID, offence_id)
    values(10111,	'18 October 2017',	103,	'A1002', 'crim107',	'33313')
  into CASES(case_id, case_date, officer_id, citizen_id, criminal_ID, offence_id)
     values(10121,	'20 October 2017',	105,	'A1005', 'crim108',	'33311')
  into CASES(case_id, case_date, officer_id, citizen_id, criminal_ID, offence_id)
      values(10131,	'22 October 2017',	101,	'A1003', 'crim103',	'33315')
  Select * from dual;
  Commit;
