using Microsoft.EntityFrameworkCore;
using PYM.models;
using PYM.Models;

namespace PYM.Services;
public class LabelService : ILabelService
{
    private PYMContext _context;
    public LabelService(PYMContext context){
        _context=context;
    }
    public ResponseModel AddLabelToIssue(int issueId, int labelId)
    {
        ResponseModel model = new ResponseModel();
        try {
                Issue issue = _context.Find<Issue>(issueId);
                Label label = _context.Find<Label>(labelId);
                if(issue==null){
                    model.IsSuccess = false;
                    model.Messsage = "Issue Not Found";
                }
                else if(label ==null){
                    model.IsSuccess = false;
                    model.Messsage = "Label Not Found";
                }
                else{
                    issue.Labels.Add(label);
                    model.Messsage = "Label Inserted Successfully";
                    _context.SaveChanges();
                    model.IsSuccess = true;
                }
        } catch (Exception ex) {
            model.IsSuccess = false;
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }

    public ResponseModel CreateLabel(string labelName)
    {
        ResponseModel model = new ResponseModel();
        try {
                Label label = new Label(){
                    LabelName = labelName
                };
                _context.Add < Label > (label);
                model.Messsage = "Label Created Successfully";
            _context.SaveChanges();
            model.IsSuccess = true;
        } catch (Exception ex) {
            model.IsSuccess = false;
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }

    public ResponseModel DeleteLabel(int labelId)
    {
        ResponseModel model = new ResponseModel();
        try {
            Label _temp = GetLabelById(labelId);
            if (_temp != null) {
                _context.Remove < Label > (_temp);
                _context.SaveChanges();
                model.IsSuccess = true;
                model.Messsage = "Label Deleted Successfully";
            } else {
                model.IsSuccess = false;
                model.Messsage = "Label Not Found";
            }
        } catch (Exception ex) {
            model.IsSuccess = false;
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }

    public List<Label> GetAllLabels()
    {
        List < Label > labels;
        try {
            labels = _context.Label.Include(s=>s.Issues).ToList();
        } catch (Exception) {
            throw;
        }
        return labels;
    }

    public Label GetLabelById(int labelId)
    {
        Label Label;
        try {
            Label = _context.Label.Include(s=>s.Issues).SingleOrDefault(s=>s.LabelId==labelId);
            // Issue = _context.Find < Issue > (issueId);
        } catch (Exception) {
            throw;
        }
        return Label;
    }

    public ResponseModel RemoveLabelFromIssue(int issueId, int labelId)
    {
        ResponseModel model = new ResponseModel();
        try {
                Issue issue = _context.Issue.Include(s=>s.Labels).FirstOrDefault(s=>s.IssueId==issueId);
                Label label = _context.Label.FirstOrDefault(s=>s.LabelId == labelId);
                if(issue==null){
                    model.IsSuccess = false;
                    model.Messsage = "Issue Not Found";
                }
                else if(label ==null){
                    model.IsSuccess = false;
                    model.Messsage = "Label Not Found";
                }
                else{
                    issue.Labels.Remove(label);
                    model.Messsage = "Label Removed Successfully";
                    _context.SaveChanges();
                    model.IsSuccess = true;
                }
        } catch (Exception ex) {
            model.IsSuccess = false;
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }

    public ResponseModel UpdateLabel(int labelId, string labelName)
    {
        ResponseModel model = new ResponseModel();
        try {
                Label label = _context.Find<Label>(labelId);
                if(label == null){
                    model.IsSuccess = false;
                    model.Messsage = "Label Not Found";
                }
                else{
                    label.LabelName = labelName;
                    model.Messsage = "Label Updated Successfully";
                    _context.SaveChanges();
                    model.IsSuccess = true;
                }
        } catch (Exception ex) {
            model.IsSuccess = false;
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }
}