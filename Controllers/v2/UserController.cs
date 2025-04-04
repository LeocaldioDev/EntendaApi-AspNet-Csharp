﻿using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PrimeiraApi.Application.ViewModel;
using PrimeiraApi.Domain.DTOs;
using PrimeiraApi.Domain.Model;
using PrimeiraApi.Domain.Model.UserAggregate;

namespace PrimeiraApi.Controllers.v2
{
    [ApiVersion(2.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PostUserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<PostUserController> _logger;
        private readonly IMapper _mapper;

        public PostUserController(IUserRepository userRepository, ILogger<PostUserController> logger, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Authorize] // Protege a rota com token
        [HttpPost]
        public IActionResult Add([FromForm]UserViewModel user) {

           

            var filepath = Path.Combine("Storage",user.photo.FileName);

            using Stream fileStream = new FileStream(filepath, FileMode.Create);

            user.photo.CopyTo(fileStream);

            var usuario = new User(user.nome, user.age,filepath);

            _userRepository.Add(usuario);

            return Ok();
        
        }

       
        [HttpGet]

        public IActionResult Get(int pageNumber, int pageQuantity) 
        {
           
            var usuarios = _userRepository.Get(pageNumber,pageQuantity);
         
            _logger.LogInformation("Elementos Obtidos");

            return Ok(usuarios);

        }
        [Authorize]
        [HttpPost]
        [Route("{id}/download")]
        public IActionResult Downloadphoto(int id) { 

            var user= _userRepository.Get(id);

            var DataBytes = System.IO.File.ReadAllBytes(user.photo);

            return File(DataBytes, "image/png");
           

        }


        [HttpGet]
        [Route("{id}")]

        public IActionResult Search(int id)
        {

            var usuarios = _userRepository.Get(id);

            var userDTO = _mapper.Map<UserDTO>(usuarios);

           // _logger.LogInformation("Elementos Obtidos");

            return Ok(userDTO);

        }


    }
}
