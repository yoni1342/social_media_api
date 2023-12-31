using AutoMapper;
using ErrorOr;
using Galacticos.Application.DTOs.Posts;
using Galacticos.Application.DTOs.Posts.Validator;
using Galacticos.Application.Features.Posts.Request.Commands;
using Galacticos.Application.Persistence.Contracts;
using Galacticos.Application.Services.ImageUpload;
using Galacticos.Domain.Entities;
using Galacticos.Domain.Errors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Galacticos.Application.Features.Posts.Handlers.Commands
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, ErrorOr<PostResponesDTO>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly ITagRepository _tagRepository;
        private readonly IPostTagRepository _postTagRepository;
        private readonly ICloudinaryService _cloudinaryService;

        public UpdatePostCommandHandler(IPostRepository postRepository, IMapper mapper, ITagRepository tagRepository, IPostTagRepository postTagRepository, ICloudinaryService cloudinaryService)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _tagRepository = tagRepository;
            _postTagRepository = postTagRepository;
            _cloudinaryService = cloudinaryService;
        }
        public async Task<ErrorOr<PostResponesDTO>> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetById(request.PostId);
            string picture = "";
            if (request.UpdatePostRequestDTO.Image != null)
                picture = _cloudinaryService.UploadImageAsync(request.UpdatePostRequestDTO.Image!).Result;

            var validator = new UpdatePostDtoValidator();
            var obj = new PostDto()
            {
                Caption = request.UpdatePostRequestDTO.Caption,
                Image = picture
            };

            var result = validator.Validate(obj);

            if (!result.IsValid)
            {
                return new ErrorOr<PostResponesDTO>().Errors;
            }

            if (post == null)
            {
                return Errors.Post.PostNotFound;
            }

            var caption = post.Caption;
            var regex = new Regex(@"#\w+");
            var hashtags = regex.Matches(caption).Select(x => x.Value).ToList();

            var tags = new List<Tag>();

            var oldPostTags = await _postTagRepository.GetPostTagsByPostId(post.Id);
            foreach (var oldPostTag in oldPostTags)
            {
                await _postTagRepository.Delete(oldPostTag);
            }

            foreach (var hashtag in hashtags)
            {
                var tag = await _tagRepository.GetTagByName(hashtag);
                if (tag == null)
                {
                    tag = new Tag { Name = hashtag };
                    await _tagRepository.Add(tag);
                }

                tags.Add(tag);
            }

            foreach (var hashtag in tags)
            {
                var tag = await _tagRepository.GetTagByName(hashtag.Name);
                if (tag != null){
                    var postTag = new PostTag { PostId = post.Id, TagId = tag.Id };
                    await _postTagRepository.Add(postTag);
                }
                else{
                    tag = new Tag { Name = hashtag.Name };
                    await _tagRepository.Add(tag);
                    var postTag = new PostTag { PostId = post.Id, TagId = tag.Id };
                    await _postTagRepository.Add(postTag);
                }
            }

            if (post.UserId != request.UserId)
            {
                return Errors.Post.PostIsNotYours;
            }
            var postToUpdate = _mapper.Map<Post>(obj);
            postToUpdate.Id = request.PostId;
            postToUpdate.UserId = request.UserId;

            var updatedPost = await _postRepository.Update(postToUpdate);
            
            var postResponseDTO = _mapper.Map<PostResponesDTO>(updatedPost);

            return postResponseDTO;

        }
    }
}
