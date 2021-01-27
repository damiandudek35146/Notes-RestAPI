﻿using AutoMapper;
using NotesOrganizer.Core.Domain;
using NotesOrganizer.Core.Repositories;
using NotesOrganizer.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotesOrganizer.Infrastructure.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;
        public NoteService(INoteRepository noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
        }
        public NoteDto Get(Guid Id)
        {
            var note = _noteRepository.Get(Id);
            return _mapper.Map<NoteDto>(note);
        }

        public IEnumerable<NoteDto> GetAll()
        {
            var notes = _noteRepository.GetAll();
            return _mapper.Map<IEnumerable<NoteDto>>(notes);

        }
        public NoteDto Created(Guid Id, string title, string content)
        {
            var note = new Note(Id, title, content);
            _noteRepository.Add(note);
            return _mapper.Map<NoteDto>(note);
        }
        public void Update(Guid Id, string title, string content)
        {
            var note = _noteRepository.Get(Id);
            note.SetTitle(title);
            note.SetContent(content);
            _noteRepository.Update(note);
        }
        public void Delete(Guid Id)
        {
            var note = _noteRepository.Get(Id);
            _noteRepository.Delete(note);
        }
    }
}
