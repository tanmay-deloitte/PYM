using PYM.Models;

namespace PYM.Services;
public interface ILabelService{
    List<Label> GetAllLabels();
    Label GetLabelById(int labelId);
    ResponseModel CreateLabel(string labelName);
    ResponseModel UpdateLabel(int labelId,string labelName);
    ResponseModel DeleteLabel(int labelId);
    ResponseModel AddLabelToIssue(int issueId,int labelId);
    ResponseModel RemoveLabelFromIssue(int issueId,int labelId);
}